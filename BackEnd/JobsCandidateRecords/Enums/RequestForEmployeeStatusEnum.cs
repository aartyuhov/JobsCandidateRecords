namespace JobsCandidateRecords.Enums
{
    /// <summary>
    /// Represents the different statuses that a request for an employee can have within the application.
    /// </summary>
    public enum RequestForEmployeeStatusEnum
    {
        /// <summary>
        /// Indicates that the request for an employee is newly created and has not yet been processed.
        /// </summary>
        New,

        /// <summary>
        /// Indicates that the request for an employee is currently being processed.
        /// </summary>
        InProcess,

        /// <summary>
        /// Indicates that the request for an employee has been closed and is no longer active.
        /// </summary>
        Closed
    }

}
