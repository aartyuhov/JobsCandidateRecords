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
import { Delete, Edit } from "@mui/icons-material";

const ListCompanies = ({ companies, onDelete, onEdit }) => {
    const [openEditModal, setOpenEditModal] = useState(false);
    const [editCompany, setEditCompany] = useState({ id: '', name: '', description: '' });

    const handleEditModalOpen = (company) => {
        setEditCompany(company);
        setOpenEditModal(true);
    };

    const handleEditModalClose = () => {
        setOpenEditModal(false);
    };

    const handleDelete = (id) => {
        onDelete(id);
    };

    const handleEdit = () => {
        onEdit(editCompany);
        setOpenEditModal(false);
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setEditCompany((prev) => ({
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
                <Table aria-label="companies table">
                    <TableHead>
                        <TableRow>
                            <TableCell>ID</TableCell>
                            <TableCell>Name</TableCell>
                            <TableCell>Description</TableCell>
                            <TableCell></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {companies.map((company) => (
                            <TableRow key={company.id}>
                                <TableCell>{company.id}</TableCell>
                                <TableCell>{company.name}</TableCell>
                                <TableCell>{company.description}</TableCell>
                                <TableCell>
                                    <Button
                                        startIcon={<Edit />}
                                        onClick={() => handleEditModalOpen(company)}
                                        variant="outlined"
                                        color="primary"
                                    >
                                        Edit
                                    </Button>
                                    <Button
                                        startIcon={<Delete />}
                                        onClick={() => handleDelete(company.id)}
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
                aria-labelledby="edit-company-modal"
                aria-describedby="form-to-edit-company"
            >
                <div style={modalStyle}>
                    <h2 id="edit-company-modal">Edit Company</h2>
                    <TextField
                        label="Name"
                        name="name"
                        value={editCompany.name}
                        onChange={handleChange}
                        fullWidth
                        margin="normal"
                    />
                    <TextField
                        label="Description"
                        name="description"
                        value={editCompany.description}
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

export default ListCompanies;