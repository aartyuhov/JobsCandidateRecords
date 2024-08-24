import React, { useState } from "react";
import { Paper, Box, Typography, IconButton, TextField, Button } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import dayjs from "dayjs";
import api from "../../services/api";

function CommentsSection({ comments, setComments, applicationId, employeeId }) {
    // Состояние для нового комментария и редактирования
    const [newComment, setNewComment] = useState("");
    const [editCommentId, setEditCommentId] = useState(null);

    // Функция для добавления или редактирования комментария
    const handleAddOrEditComment = async () => {
        if (newComment.trim()) {
            if (editCommentId) {
                // Редактирование существующего комментария
                const updatedComment = {
                    id: editCommentId,
                    applicationId: applicationId,
                    employeeId: employeeId,
                    text: newComment,
                    creationDate: dayjs().toISOString(), // Обновляем дату редактирования
                    authorName: comments.find(comment => comment.id === editCommentId)?.authorName || "Unknown",
                };

                try {
                    await api.put(`/api/NotesDTO`, updatedComment);
                    setComments(
                        comments.map((comment) =>
                            comment.id === editCommentId ? updatedComment : comment
                        )
                    );
                    setEditCommentId(null); // Сброс режима редактирования
                    setNewComment(""); // Очистка поля ввода
                } catch (error) {
                    console.error('Error updating comment:', error);
                }
            } else {
                // Добавление нового комментария
                const newCommentObj = {
                    id: 0,
                    applicationId: applicationId,
                    employeeId: employeeId,
                    text: newComment,
                    creationDate: dayjs().toISOString(),
                    authorName: ""
                };

                const postComment = async (data) => {
                    try {
                        await api.post(`/api/NotesDTO`,data);
                    } catch (error) {
                        console.error('Error fetching data:', error);
                    }
                }
                postComment(newCommentObj).catch(error => console.error(error));
                newCommentObj.id = comments.length + 1;
                newCommentObj.authorName = "Current User";
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
    const handleDeleteComment = async (id) => {
        try {
            await api.delete(`/api/NotesDTO/${id}`);
            setComments(comments.filter((comment) => comment.id !== id));
        } catch (error) {
            console.error('Error updating comment:', error);
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
                            {comment.authorName}
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
                            {dayjs(comment.creationDate).format('YYYY-MM-DD HH:mm:ss')}
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
