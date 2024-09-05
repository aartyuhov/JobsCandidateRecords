import React, { useState } from 'react';
import { Button, TextField } from "@mui/material";

const CreateCompany = ({ onCreate }) => {
    const [newCompany, setNewCompany] = useState({ name: '', description: '' });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setNewCompany((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    const handleCreate = (e) => {
        e.preventDefault();
        onCreate(newCompany);
        setNewCompany({ name: '', description: '' });
    };

    return (
        <form autoComplete="off" className="d-flex align-content-start gap-4 p-3" onSubmit={handleCreate}>
            <TextField
                label="Name"
                name="name"
                value={newCompany.name}
                onChange={handleChange}
                fullWidth
                margin="normal"
                required
            />
            <TextField
                label="Description"
                name="description"
                value={newCompany.description}
                onChange={handleChange}
                fullWidth
                margin="normal"
            />
            <Button variant="contained" color="primary" className="w-25 my-auto" type="submit">
                Create
            </Button>
        </form>
    );
};

export default CreateCompany;
