import React, { useState } from "react";
import { TextField, Button, Paper, Box, Typography, Container } from "@mui/material";
import dayjs from 'dayjs'; // Библиотека для форматирования даты

function CandidateProfile() {
    // Состояние для данных кандидата
    const [candidate, setCandidate] = useState({
        id: 1,
        firstName: "John",
        lastName: "Doe",
        dateOfBirth: "1990-01-01",
        gender: "Male",
        email: "john.doe@example.com",
        phone: "+1234567890",
        address: "123 Main St, Springfield",
        aboutInfo: "Experienced developer with a passion for creating web applications.",
    });

    // Состояние для комментариев
    const [comments, setComments] = useState([]);
    const [newComment, setNewComment] = useState("");

    // Функция для обновления данных кандидата
    const handleChange = (e) => {
        const { name, value } = e.target;
        setCandidate({ ...candidate, [name]: value });
    };

    // Функция для добавления нового комментария
    const handleAddComment = () => {
        if (newComment.trim()) {
            const newCommentObj = {
                id: comments.length + 1,
                text: newComment,
                createdDate: dayjs().format('YYYY-MM-DD HH:mm:ss'),
                author: "Current User" // Это можно заменить на имя реального пользователя
            };
            setComments([...comments, newCommentObj]);
            setNewComment("");
        }
    };

    return (
        <Paper className="mt-4 w-75" sx={{ backgroundColor: "#f0f0f0" }}>
            <div className="d-flex gap-4 align-content-sm-center">
                {/* Левая колонка - Карточка кандидата */}
                <div className="col-md-12 w-75">
                    <Paper
                        sx={{
                            padding: "20px",
                            backgroundColor: "#f0f0f0",
                            display: "flex",
                            flexDirection: "column"
                        }}
                    >
                        <Typography variant="h4" sx={{ marginBottom: "15px" }}>
                            Edit Candidate Profile
                        </Typography>
                        <Box
                            component="form"
                            sx={{
                                "& .MuiTextField-root": { marginBottom: "15px" },
                            }}
                        >
                            <TextField
                                label="First Name"
                                fullWidth
                                name="firstName"
                                value={candidate.firstName}
                                onChange={handleChange}
                                variant="outlined"
                            />
                            <TextField
                                label="Last Name"
                                fullWidth
                                name="lastName"
                                value={candidate.lastName}
                                onChange={handleChange}
                                variant="outlined"
                            />
                            <TextField
                                label="Date of Birth"
                                fullWidth
                                name="dateOfBirth"
                                value={candidate.dateOfBirth}
                                onChange={handleChange}
                                variant="outlined"
                            />
                            <TextField
                                label="Gender"
                                fullWidth
                                name="gender"
                                value={candidate.gender}
                                onChange={handleChange}
                                variant="outlined"
                            />
                            <TextField
                                label="Email"
                                fullWidth
                                name="email"
                                value={candidate.email}
                                onChange={handleChange}
                                variant="outlined"
                            />
                            <TextField
                                label="Phone"
                                fullWidth
                                name="phone"
                                value={candidate.phone}
                                onChange={handleChange}
                                variant="outlined"
                            />
                            <TextField
                                label="Address"
                                fullWidth
                                name="address"
                                value={candidate.address}
                                onChange={handleChange}
                                variant="outlined"
                            />
                            <TextField
                                label="About"
                                fullWidth
                                name="aboutInfo"
                                multiline
                                rows={4}
                                value={candidate.aboutInfo}
                                onChange={handleChange}
                                variant="outlined"
                            />
                        </Box>
                        <Button
                            variant="contained"
                            color="primary"
                            sx={{ marginTop: "10px", marginRight:"1em", alignSelf: "flex-end" }}
                        >
                            Update
                        </Button>
                    </Paper>
                </div>

                {/* Правая колонка - Чат с комментариями */}
                <div className="col-md-6 w-50">
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
                            Comments
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
                            {comments.map((comment) => (
                                <Box
                                    key={comment.id}
                                    sx={{
                                        backgroundColor: "#ffffff",
                                        borderRadius: "10px",
                                        padding: "10px",
                                        marginBottom: "10px",
                                        boxShadow: "0 1px 3px rgba(0, 0, 0, 0.2)",
                                    }}
                                >
                                    <Typography variant="body2" sx={{ fontWeight: "bold" }}>
                                        {comment.author}
                                    </Typography>
                                    <Typography variant="body2" sx={{ marginBottom: "5px" }}>
                                        {comment.text}
                                    </Typography>
                                    <Typography variant="caption" color="textSecondary">
                                        {comment.createdDate}
                                    </Typography>
                                </Box>
                            ))}
                        </Box>
                        <TextField
                            label="Add a comment"
                            fullWidth
                            multiline
                            rows={2}
                            variant="outlined"
                            value={newComment}
                            onChange={(e) => setNewComment(e.target.value)}
                        />
                        <Button
                            variant="contained"
                            color="primary"
                            sx={{ marginTop: "10px", alignSelf: "flex-end" }}
                            onClick={handleAddComment}
                        >
                            Add Comment
                        </Button>
                    </Paper>
                </div>
            </div>
        </Paper>
    );
}

export default CandidateProfile;
