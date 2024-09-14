import React, { useEffect, useState } from 'react';
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
import { Delete, Add } from "@mui/icons-material";
import api from '../../services/api'; // Убедитесь, что путь к вашему API правильный

const ListRoles = () => {
    const [roles, setRoles] = useState([]);
    const [openAddModal, setOpenAddModal] = useState(false);
    const [newRole, setNewRole] = useState({ id: '', name: '' });

    useEffect(() => {
        const fetchRoles = async () => {
            try {
                const response = await api.get('/api/Roles');
                setRoles(response.data);
            } catch (error) {
                console.error('Failed to fetch roles:', error);
            }
        };

        fetchRoles();
    }, []);

    const handleAddModalOpen = () => {
        setNewRole({ id: '', name: '' });
        setOpenAddModal(true);
    };

    const handleAddModalClose = () => {
        setOpenAddModal(false);
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setNewRole((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    const handleAdd = async () => {
        try {
            await api.post('/api/Roles', {
                roleName: newRole.name
            });
            const response = await api.get('/api/Roles');
            setRoles(response.data);
            setOpenAddModal(false);
        } catch (error) {
            console.error('Failed to add role:', error);
        }
    };

    const handleDelete = async (name) => {
        try {
            await api.delete(`/api/Roles/${name}`);
            // Re-fetch roles to update the list
            const response = await api.get('/api/Roles');
            setRoles(response.data);
        } catch (error) {
            console.error('Failed to delete role:', error);
        }
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
            <Button
                startIcon={<Add />}
                onClick={handleAddModalOpen}
                variant="contained"
                color="primary"
                style={{ margin: '16px' }}
            >
                Add Role
            </Button>
            <TableContainer component={Paper}>
                <Table aria-label="roles table">
                    <TableHead>
                        <TableRow>
                            <TableCell>ID</TableCell>
                            <TableCell>Name</TableCell>
                            <TableCell></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {roles.map((role) => (
                            <TableRow key={role.id}>
                                <TableCell>{role.id}</TableCell>
                                <TableCell>{role.name}</TableCell>
                                <TableCell>
                                    <Button
                                        startIcon={<Delete />}
                                        onClick={() => handleDelete(role.name)}
                                        variant="outlined"
                                        color="secondary"
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
                open={openAddModal}
                onClose={handleAddModalClose}
                aria-labelledby="add-role-modal"
                aria-describedby="form-to-add-role"
            >
                <div style={modalStyle}>
                    <h2 id="add-role-modal">Add Role</h2>
                    <TextField
                        label="Name"
                        name="name"
                        value={newRole.name}
                        onChange={handleChange}
                        fullWidth
                        margin="normal"
                    />
                    <Button variant="contained" color="primary" onClick={handleAdd}>
                        Add Role
                    </Button>
                </div>
            </Modal>
        </>
    );
};

export default ListRoles;
