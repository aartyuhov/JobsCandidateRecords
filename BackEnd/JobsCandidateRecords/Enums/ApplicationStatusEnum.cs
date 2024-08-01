namespace JobsCandidateRecords.Enums
{
    /// <summary>
    /// Represents the different statuses an application can have.
    /// </summary>
    public enum ApplicationStatusEnum
    {
        /// <summary>
        /// The application is new.
        /// </summary>
        New,

        /// <summary>
        /// The application is being processed.
        /// </summary>
        InProcess,

        /// <summary>
        /// The application was rejected by HR.
        /// </summary>
        RejectedHR,

        /// <summary>
        /// The application is under manager's approval.
        /// </summary>
        UnderManagerApproval,

        /// <summary>
        /// The application was rejected by the manager.
        /// </summary>
        RejectedManager,

        /// <summary>
        /// The application is in the interview process.
        /// </summary>
        InterviewProcess,

        /// <summary>
        /// The application is under approval after the interview.
        /// </summary>
        UnderApprovalAfterInterview,

        /// <summary>
        /// The application was rejected after the interview.
        /// </summary>
        RejectedAfterInterview,

        /// <summary>
        /// The application is under final approval.
        /// </summary>
        UnderFinalApproval,

        /// <summary>
        /// The application was rejected during the final approval.
        /// </summary>
        RejectedFinal,

        /// <summary>
        /// The application was approved during the final approval.
        /// </summary>
        ApprovedFinal,

        /// <summary>
        /// The application was not selected.
        /// </summary>
        NotSelected
    }
}
