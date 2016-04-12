using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Design;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;
using System.Globalization;
using System.IO;
using System.Linq;
using Ises.Core.Common;
using Ises.Core.Utils;

namespace Ises.Data.Migrations
{
    public class ExtendedCodeGenerator : CSharpMigrationCodeGenerator
    {
        protected override void WriteClassStart(string @namespace, string className, IndentedTextWriter writer, string @base, bool designer = false, IEnumerable<string> namespaces = null)
        {
            if (@namespace.IsNotNullOrWhiteSpace())
            {
                writer.Write("namespace ");
                writer.WriteLine(@namespace);
                writer.WriteLine("{");
                writer.Indent++;
            }

            (namespaces ?? GetDefaultNamespaces(designer)).Each(n => writer.WriteLine("using " + n + ";"));

            if (namespaces != null && namespaces.Any()) writer.WriteLine();

            if (designer)
            {
                writer.WriteLine("[GeneratedCode(\"EntityFramework.Migrations\", \"{0}\")]", typeof(CSharpMigrationCodeGenerator).Assembly.GetInformationalVersion());
            }

            writer.Write("public ");

            if (designer)
            {
                writer.Write("sealed ");
            }

            writer.Write("partial class ");
            writer.Write(className);
            writer.Write(" : ");
            writer.Write(@base);
            writer.WriteLine();
            writer.WriteLine("{");
            writer.Indent++;
        }

        protected override string Generate(IEnumerable<MigrationOperation> operations, string @namespace, string className)
        {
            var migrationOperations = operations as MigrationOperation[] ?? operations.ToArray();
            var newTableForeignKeys
                = (from ct in migrationOperations.OfType<CreateTableOperation>()
                   from cfk in migrationOperations.OfType<AddForeignKeyOperation>()
                   where ct.Name.EqualsIgnoreCase(cfk.DependentTable)
                   select Tuple.Create(ct, cfk)).ToList();

            var newTableIndexes
                = (from ct in migrationOperations.OfType<CreateTableOperation>()
                   from ci in migrationOperations.OfType<CreateIndexOperation>()
                   where ct.Name.EqualsIgnoreCase(ci.Table)
                   select Tuple.Create(ct, ci)).ToList();

            using (var stringWriter = new StringWriter(CultureInfo.InvariantCulture))
            {
                using (var writer = new IndentedTextWriter(stringWriter))
                {
                    WriteClassStart(@namespace, className, writer, "BaseDbMigration");

                    writer.WriteLine("public override void Upgrade()");
                    writer.WriteLine("{");
                    writer.Indent++;

                    migrationOperations
                        .Except(newTableForeignKeys.Select(t => t.Item2))
                        .Except(newTableIndexes.Select(t => t.Item2))
                        .Each<dynamic>(o => Generate(o, writer));

                    writer.Indent--;
                    writer.WriteLine("}");

                    writer.WriteLine();

                    writer.WriteLine("public override void Downgrade()");
                    writer.WriteLine("{");
                    writer.Indent++;

                    operations
                        = migrationOperations
                            .Select(o => o.Inverse)
                            .Where(o => o != null)
                            .Reverse();

                    var hasUnsupportedOperations
                        = operations.Any(o => o is NotSupportedOperation);

                    operations
                        .Where(o => !(o is NotSupportedOperation))
                        .Each<dynamic>(o => Generate(o, writer));

                    if (hasUnsupportedOperations)
                    {
                        writer.Write("throw new NotSupportedException(");
                        writer.Write(Generate("ScaffoldSprocInDownNotSupported"));
                        writer.WriteLine(");");
                    }

                    writer.Indent--;
                    writer.WriteLine("}");

                    WriteClassEnd(@namespace, writer);
                }

                return stringWriter.ToString();
            }
        }

        protected override IEnumerable<string> GetDefaultNamespaces(bool designer = false)
        {
            var namespaces = new List<string>();
            if (designer)
            {
                namespaces.Add("System.CodeDom.Compiler");
                namespaces.Add("System.Data.Entity.Migrations.Infrastructure");
                namespaces.Add("System.Resources");
            }

            return namespaces.OrderBy(n => n);
        }
    }
}