import React, { useState, useEffect } from 'react';
import {Button, TextField} from "@mui/material";

const EditPosition = ({ position, onUpdate }) => {
    const [editPosition, setEditPosition] = useState({});

    useEffect(() => {
        setEditPosition(position);
    }, [position]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setEditPosition((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    const handleUpdate = () => {
        onUpdate(editPosition);
    };

    return (
        <form noValidate autoComplete="off" style={{ padding: '16px', maxWidth: '400px', margin: 'auto' }}>
            <TextField
                label="Title"
                name="title"
                value={editPosition.title || ''}
                onChange={handleChange}
                fullWidth
                margin="normal"
            />
            <TextField
                label="Responsibilities"
                name="responsibilities"
                value={editPosition.responsibilities || ''}
                onChange={handleChange}
                fullWidth
                margin="normal"
            />
            <TextField
                label="Department ID"
                name="departmentId"
                type="number"
                value={editPosition.departmentId || ''}
                onChange={handleChange}
                fullWidth
                margin="normal"
            />
            <Button variant="contained" color="primary" onClick={handleUpdate}>
                Update
            </Button>
        </form>
    );
};

export default EditPosition;
