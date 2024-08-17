import React, { useState } from "react";
import { Paper, Box, Typography, IconButton, TextField, Button } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import dayjs from "dayjs";

function CommentsSection({ comments, setComments }) {
    // Состояние для нового комментария и редактирования
    const [newComment, setNewComment] = useState("");
    const [editCommentId, setEditCommentId] = useState(null);

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
    );
}

export default CommentsSection;
