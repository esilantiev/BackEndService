using System;

namespace RefactorThis.GraphDiff.CustomExceptions
{
    public class DbUpdateConcurrencyException<T> : Exception
    {
        public T ClientEntity { get; set; }
        public T DatabaseEntity { get; set; }
        public DbUpdateConcurrencyException() : base() { }
        public DbUpdateConcurrencyException(string message, T clientEntity, T databaseEntity) : base(message) 
        {
            this.ClientEntity = clientEntity;
            this.DatabaseEntity = databaseEntity;
        }
        public DbUpdateConcurrencyException(string message, Exception inner) : base(message, inner) { }
    }
}
