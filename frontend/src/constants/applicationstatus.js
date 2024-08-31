export const ApplicationStatus = {
    NEW: { label: 'New', value: 0 },
    IN_PROCESS: { label: 'InProcess', value: 1 },
    REJECTED_HR: { label: 'RejectedHR', value: 2 },
    UNDER_MANAGER_APPROVAL: { label: 'UnderManagerApproval', value: 3 },
    REJECTED_MANAGER: { label: 'RejectedManager', value: 4 },
    INTERVIEW_PROCESS: { label: 'InterviewProcess', value: 5 },
    UNDER_APPROVAL_AFTER_INTERVIEW: { label: 'UnderApprovalAfterInterview', value: 6 },
    REJECTED_AFTER_INTERVIEW: { label: 'RejectedAfterInterview', value: 7 },
    UNDER_FINAL_APPROVAL: { label: 'UnderFinalApproval', value: 8 },
    REJECTED_FINAL: { label: 'RejectedFinal', value: 9 },
    APPROVED_FINAL: { label: 'ApprovedFinal', value: 10 },
    NOT_SELECTED: { label: 'NotSelected', value: 11 }
};

// Удобные для пользователя названия статусов
export const UserFriendlyStatusLabels = {
    NEW: 'New',
    IN_PROCESS: 'In process',
    REJECTED_HR: 'Rejected by HR',
    UNDER_MANAGER_APPROVAL: 'Under manager approval',
    REJECTED_MANAGER: 'Rejected by manager',
    INTERVIEW_PROCESS: 'Interview process',
    UNDER_APPROVAL_AFTER_INTERVIEW: 'Under approval after interview',
    REJECTED_AFTER_INTERVIEW: 'Rejected after interview',
    UNDER_FINAL_APPROVAL: 'Under final approval',
    REJECTED_FINAL: 'Rejected final',
    APPROVED_FINAL: 'Approved final',
    NOT_SELECTED: 'Not selected'
};
