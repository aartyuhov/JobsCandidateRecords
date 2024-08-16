import React, { useState } from "react";
import { TextField, Button, Paper, Box, Typography, IconButton, InputLabel, Select, MenuItem, FormControl } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import dayjs from "dayjs";
import BasicDatePicker from "../small-components/BasicDatePicker";

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

    // Состояние для обновленных данных
    const [updatedCandidate, setUpdatedCandidate] = useState({ ...candidate });

    // Состояние для комментариев
    const [comments, setComments] = useState([]);
    const [newComment, setNewComment] = useState("");
    const [editCommentId, setEditCommentId] = useState(null);

    // Функция для обновления данных кандидата в форме
    const handleChange = (e) => {
        const { name, value } = e.target;
        setUpdatedCandidate({ ...updatedCandidate, [name]: value });
    };

    // Функция для добавления или редактирования комментария
    const handleAddOrEditComment = () => {
        if (newComment.trim()) {
            if (editCommentId) {
                // Редактирование существующего комментария
                setComments(
                    comments.map((comment) =>
                        comment.id === editCommentId ? { ...comment, text: newComment } : comment
                    )
                );
                setEditCommentId(null); // Сброс режима редактирования
            } else {
                // Добавление нового комментария
                const newCommentObj = {
                    id: comments.length + 1,
                    text: newComment,
                    createdDate: dayjs().format("YYYY-MM-DD HH:mm:ss"),
                    author: "Current User", // Здесь можно заменить на имя реального пользователя
                };
                setComments([...comments, newCommentObj]);
            }
            setNewComment(""); // Очистка поля ввода
        }
    };

    // Функция для редактирования комментария
    const handleEditComment = (id, text) => {
        setEditCommentId(id);
        setNewComment(text);
    };

    // Функция для удаления комментария
    const handleDeleteComment = (id) => {
        setComments(comments.filter((comment) => comment.id !== id));
    };

    // Функция для обновления профиля кандидата
    const handleUpdateProfile = () => {
        setCandidate(updatedCandidate);
        // Здесь можно добавить логику для отправки обновленных данных на сервер
        console.log("Profile updated:", updatedCandidate);
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
                                value={updatedCandidate.firstName}
                                onChange={handleChange}
                                variant="outlined"
                                required
                            />
                            <TextField
                                label="Last Name"
                                fullWidth
                                name="lastName"
                                value={updatedCandidate.lastName}
                                onChange={handleChange}
                                variant="outlined"
                                required
                            />
                            <BasicDatePicker
                                label="Date of Birth"
                                name="dateOfBirth"
                                defaultValue={updatedCandidate.dateOfBirth}
                                value={updatedCandidate.dateOfBirth}
                                className="col-md-12"
                                required
                            />
                            <FormControl variant="outlined" fullWidth required sx={{ marginBottom: "1em" }}>
                                <InputLabel>Gender</InputLabel>
                                <Select
                                    label="Gender"
                                    name="gender"
                                    onChange={handleChange}
                                    defaultValue={updatedCandidate.gender}
                                >
                                    <MenuItem value="Male">Male</MenuItem>
                                    <MenuItem value="Female">Female</MenuItem>
                                </Select>
                            </FormControl>
                            <TextField
                                label="Email"
                                fullWidth
                                name="email"
                                value={updatedCandidate.email}
                                onChange={handleChange}
                                variant="outlined"
                            />
                            <TextField
                                label="Phone"
                                fullWidth
                                name="phone"
                                value={updatedCandidate.phone}
                                onChange={handleChange}
                                variant="outlined"
                            />
                            <TextField
                                label="Address"
                                fullWidth
                                name="address"
                                value={updatedCandidate.address}
                                onChange={handleChange}
                                variant="outlined"
                            />
                            <TextField
                                label="About"
                                fullWidth
                                name="aboutInfo"
                                multiline
                                rows={4}
                                value={updatedCandidate.aboutInfo}
                                onChange={handleChange}
                                variant="outlined"
                            />

                            {/* Кнопка Update */}
                            <Button
                                variant="contained"
                                color="primary"
                                sx={{ marginTop: "10px" }}
                                onClick={handleUpdateProfile}
                            >
                                Update
                            </Button>
                        </Box>
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
                                        wordWrap: "break-word",
                                        wordBreak: "break-word",
                                        position: "relative",
                                    }}
                                >
                                    <Typography variant="body2" sx={{ fontWeight: "bold" }}>
                                        {comment.author}
                                    </Typography>
                                    <Typography
                                        variant="body2"
                                        sx={{
                                            marginBottom: "5px",
                                            whiteSpace: "pre-wrap",
                                        }}
                                    >
                                        {comment.text}
                                    </Typography>
                                    <Typography variant="caption" color="textSecondary">
                                        {comment.createdDate}
                                    </Typography>
                                    <Box
                                        sx={{
                                            position: "absolute",
                                            bottom: "5px",
                                            right: "5px",
                                            display: "flex",
                                            gap: "5px",
                                        }}
                                    >
                                        <IconButton
                                            size="small"
                                            onClick={() => handleEditComment(comment.id, comment.text)}
                                        >
                                            <EditIcon fontSize="small" />
                                        </IconButton>
                                        <IconButton
                                            size="small"
                                            onClick={() => handleDeleteComment(comment.id)}
                                        >
                                            <DeleteIcon fontSize="small" />
                                        </IconButton>
                                    </Box>
                                </Box>
                            ))}
                        </Box>
                        <TextField
                            label={editCommentId ? "Edit comment" : "Add a comment"}
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
                            onClick={handleAddOrEditComment}
                        >
                            {editCommentId ? "Update Comment" : "Add Comment"}
                        </Button>
                    </Paper>
                </div>
            </div>
        </Paper>
    );
}

export default CandidateProfile;
