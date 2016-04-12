using System.ComponentModel;

namespace Ises.Contracts.Common
{
    public enum IsolationCertificateStatus
    {
        [Description("Draft")]
        Draft,
        [Description("In progress")]
        InProgress,
        [Description("Completed")]
        Completed
    }
    public enum IsolationCertificateType
    {
        [Description("Full")]
        Full,
        [Description("Personal")]
        Personal
    }

    public enum YesNoOption
    {
        [Description("Yes")]
        Yes,
        [Description("No")]
        No,
    }

    public enum WorkCertificateStatus
    {
        [Description("Draft")]
        Draft,
        [Description("In progress")]
        InProgress,
        [Description("Need to be approved")]
        NeedToBeApproved
    }

    public enum WorkCertificateType
    {
        [Description("Permit")]
        Permit,
        [Description("Routine Template")]
        RoutineTemplate
    }

    public enum WorkCertificateIsolationType
    {
        [Description("Full")]
        Full,
        [Description("Personal")]
        Personal,
        [Description("N/A")]
        Na
    }

    public enum UserShift
    {
        [Description("Day")]
        Day,
        [Description("Night")]
        Night
    }

    public enum EntityType
    {
        NotAssigned,
        WorkCertificate,
        IsolationCertificate,
        User
    }

    public enum HazardControlType
    {
        [Description("Pre")]
        Pre,
        [Description("Supp")]
        Supp
    }

    public enum CrossReferenceConnectionType
    {
        [Description("Live - Live")]
        LiveLive,
        [Description("Live - Not Live")]
        LiveNotLive,
        [Description("Start - Finish")]
        StartFinish,
        [Description("SCC")]
        Scc
    }

    public enum BaseCertificateType
    {
        [Description("WorkCertificate")]
        WorkCertificate,
        [Description("IsolationCertificate")]
        IsolationCertificate
    }

    public enum ActionStatus
    {
        [Description("Not Defined")]
        NotDefined,
        [Description("Processed")]
        Processed,
        [Description("WasRead")]
        WasRead,
        [Description("Approved")]
        Approved,
        [Description("Declined")]
        Declined
    }

    public enum IsolationPointLoLc
    {
        [Description("Process")]
        Process,
        [Description("Mechanical")]
        Processed
    }
}
