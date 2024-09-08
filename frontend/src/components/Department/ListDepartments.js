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

const ListDepartments = ({ departments, onDelete, onEdit }) => {
    const [openEditModal, setOpenEditModal] = useState(false);
    const [editDepartment, setEditDepartment] = useState({ id: '', name: '', description: '', companyId: 0, companyName: '' });

    const handleEditModalOpen = (department) => {
        setEditDepartment(department);
        setOpenEditModal(true);
    };

    const handleEditModalClose = () => {
        setOpenEditModal(false);
    };

    const handleDelete = (id) => {
        onDelete(id);
    };

    const handleEdit = () => {
        onEdit(editDepartment);
        setOpenEditModal(false);
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setEditDepartment((prev) => ({
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

    return (
        <>
            <TableContainer component={Paper}>
                <Table aria-label="departments table">
                    <TableHead>
                        <TableRow>
                            <TableCell>ID</TableCell>
                            <TableCell>Name</TableCell>
                            <TableCell>Description</TableCell>
                            <TableCell>Company ID</TableCell>
                            <TableCell>Company Name</TableCell>
                            <TableCell></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {departments.map((department) => (
                            <TableRow key={department.id}>
                                <TableCell>{department.id}</TableCell>
                                <TableCell>{department.name}</TableCell>
                                <TableCell>{department.description}</TableCell>
                                <TableCell>{department.companyId}</TableCell>
                                <TableCell>{department.companyName}</TableCell>
                                <TableCell>
                                    <Button
                                        startIcon={<Edit />}
                                        onClick={() => handleEditModalOpen(department)}
                                        variant="outlined"
                                        color="primary"
                                    >
                                        Edit
                                    </Button>
                                    <Button
                                        startIcon={<Delete />}
                                        onClick={() => handleDelete(department.id)}
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
                aria-labelledby="edit-department-modal"
                aria-describedby="form-to-edit-department"
            >
                <div style={modalStyle}>
                    <h2 id="edit-department-modal">Edit Department</h2>
                    <TextField
                        label="Name"
                        name="name"
                        value={editDepartment.name}
                        onChange={handleChange}
                        fullWidth
                        margin="normal"
                    />
                    <TextField
                        label="Description"
                        name="description"
                        value={editDepartment.description}
                        onChange={handleChange}
                        fullWidth
                        margin="normal"
                    />
                    <TextField
                        label="Company ID"
                        name="companyId"
                        type="number"
                        value={editDepartment.companyId}
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

export default ListDepartments;
