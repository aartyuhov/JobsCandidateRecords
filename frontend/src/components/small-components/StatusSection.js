import React from "react";
import { Paper, Box, Typography } from "@mui/material";
import dayjs from "dayjs";
import {UserFriendlyStatusLabels} from "../../constants/applicationstatus";

function StatusSection({ statuses }) {
    const getStatusName = (statusValue) => {
        switch (statusValue) {
            case 0:
                return UserFriendlyStatusLabels.NEW;
            case 1:
                return UserFriendlyStatusLabels.IN_PROCESS;
            case 2:
                return UserFriendlyStatusLabels.REJECTED_HR;
            case 3:
                return UserFriendlyStatusLabels.UNDER_MANAGER_APPROVAL;
            case 4:
                return UserFriendlyStatusLabels.REJECTED_MANAGER;
            case 5:
                return UserFriendlyStatusLabels.INTERVIEW_PROCESS;
            case 6:
                return UserFriendlyStatusLabels.UNDER_APPROVAL_AFTER_INTERVIEW;
            case 7:
                return UserFriendlyStatusLabels.REJECTED_AFTER_INTERVIEW;
            case 8:
                return UserFriendlyStatusLabels.UNDER_FINAL_APPROVAL;
            case 9:
                return UserFriendlyStatusLabels.REJECTED_FINAL;
            case 10:
                return UserFriendlyStatusLabels.APPROVED_FINAL;
            case 11:
                return UserFriendlyStatusLabels.NOT_SELECTED;
            default:
                return "Unknown";
        }
    };

    return (
        <Paper
            sx={{
                padding: "20px",
                height: "100%",
                display: "flex",
                flexDirection: "column",
                backgroundColor: "#fafafa",
            }}
        >
            <Typography variant="h6" sx={{ marginBottom: "15px" }}>
                Status History
            </Typography>
            <Box
                sx={{
                    flexGrow: 1,
                    overflowY: "auto",
                    marginBottom: "15px",
                    padding: "10px",
                    backgroundColor: "#e0e0e0",
                    borderRadius: "4px",
                }}
            >
                {statuses.map((status) => (
                    <Box
                        key={status.id}
                        sx={{
                            backgroundColor: "#ffffff",
                            borderRadius: "10px",
                            padding: "10px",
                            marginBottom: "10px",
                            boxShadow: "0 1px 3px rgba(0, 0, 0, 0.2)",
                            wordWrap: "break-word",
                            wordBreak: "break-word",
                            position: "relative",
                        }}
                    >
                        <Typography variant="body2"
                                    sx={{ fontWeight: "bold",
                                            padding: "4px"
                                    }}>
                            {status.employeeFullname ?? ''}
                        </Typography>
                        <Typography variant="body2" sx={{ fontWeight: "bold" }}>
                            {getStatusName(status.applicationStatus)}
                        </Typography>
                        <Typography variant="caption" color="textSecondary">
                            {dayjs(status.decisionDate).format('YYYY-MM-DD HH:mm:ss')}
                        </Typography>
                    </Box>
                ))}
            </Box>
        </Paper>
    );
}

export default StatusSection;
