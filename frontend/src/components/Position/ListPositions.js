import React, { useState } from 'react';
import {
    Button,
    Modal,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    TextField
} from "@mui/material";
import {Delete, Edit} from "@mui/icons-material";

const ListPositions = ({ positions, onDelete, onEdit }) => {
    const [openEditModal, setOpenEditModal] = useState(false);
    const [editPosition, setEditPosition] = useState({ id: '', title: '', responsibilities: '', departmentId: 0 });

    const handleEditModalOpen = (position) => {
        setEditPosition(position);
        setOpenEditModal(true);
    };

    const handleEditModalClose = () => {
        setOpenEditModal(false);
    };

    const handleDelete = (id) => {
        onDelete(id);
    };

    const handleEdit = () => {
        onEdit(editPosition);
        setOpenEditModal(false);
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setEditPosition((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    const modalStyle = {
        position: 'absolute',
        width: 400,
        backgroundColor: 'white',
        border: '2px solid #000',
        boxShadow: '24px',
        padding: '16px 32px 24px',
        top: '50%',
        left: '50%',
        transform: 'translate(-50%, -50%)',
    };

    console.log(positions);
    return (
        <>
            <TableContainer component={Paper}>
                <Table aria-label="positions table">
                    <TableHead>
                        <TableRow>
                            <TableCell>ID</TableCell>
                            <TableCell>Title</TableCell>
                            <TableCell>Responsibilities</TableCell>
                            <TableCell>Department ID</TableCell>
                            <TableCell></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {positions.map((position) => (
                            <TableRow key={position.id}>
                                <TableCell>{position.id}</TableCell>
                                <TableCell>{position.title}</TableCell>
                                <TableCell>{position.responsibilities}</TableCell>
                                <TableCell>{position.departmentId}</TableCell>
                                <TableCell>
                                    <Button
                                        startIcon={<Edit />}
                                        onClick={() => handleEditModalOpen(position)}
                                        variant="outlined"
                                        color="primary"
                                    >
                                        Edit
                                    </Button>
                                    <Button
                                        startIcon={<Delete />}
                                        onClick={() => handleDelete(position.id)}
                                        variant="outlined"
                                        color="secondary"
                                        style={{ marginLeft: '10px' }}
                                    >
                                        Delete
                                    </Button>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>

            <Modal
                open={openEditModal}
                onClose={handleEditModalClose}
                aria-labelledby="edit-position-modal"
                aria-describedby="form-to-edit-position"
            >
                <div style={modalStyle}>
                    <h2 id="edit-position-modal">Edit Position</h2>
                    <TextField
                        label="Title"
                        name="title"
                        value={editPosition.title}
                        onChange={handleChange}
                        fullWidth
                        margin="normal"
                    />
                    <TextField
                        label="Responsibilities"
                        name="responsibilities"
                        value={editPosition.responsibilities}
                        onChange={handleChange}
                        fullWidth
                        margin="normal"
                    />
                    <TextField
                        label="Department ID"
                        name="departmentId"
                        type="number"
                        value={editPosition.departmentId}
                        onChange={handleChange}
                        fullWidth
                        margin="normal"
                    />
                    <Button variant="contained" color="primary" onClick={handleEdit}>
                        Save Changes
                    </Button>
                </div>
            </Modal>
        </>
    );
};

export default ListPositions;
