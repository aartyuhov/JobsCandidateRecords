import React, { useState } from "react";
import { Paper, Box, Typography } from "@mui/material";
import dayjs from "dayjs";

function StatusSection() {
    // Тестовые данные для статусов
    const [statuses] = useState([
        {
            id: 1,
            statusName: "Application Submitted",
            changeDate: dayjs().subtract(7, 'day').toISOString(),
        },
        {
            id: 2,
            statusName: "Under Review",
            changeDate: dayjs().subtract(3, 'day').toISOString(),
        },
        {
            id: 3,
            statusName: "Approved",
            changeDate: dayjs().toISOString(),
        },
    ]);

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
                        <Typography variant="body2" sx={{ fontWeight: "bold" }}>
                            {status.statusName}
                        </Typography>
                        <Typography variant="caption" color="textSecondary">
                            {dayjs(status.changeDate).format('YYYY-MM-DD HH:mm:ss')}
                        </Typography>
                    </Box>
                ))}
            </Box>
        </Paper>
    );
}

export default StatusSection;
