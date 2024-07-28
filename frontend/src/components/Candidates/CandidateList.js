// src/CandidateList.js

import React, { useState } from 'react';
import {
    Container,
    TextField,
    Tabs,
    Tab,
    Paper,
    Avatar,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    TableSortLabel,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    Button,
    IconButton,
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    Card, Chip
} from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import 'bootstrap/dist/css/bootstrap.min.css';
import SearchBar from "../small-components/SearchBar";

const initialCandidates = [
    {
        id: 1,
        firstName: 'John',
        lastName: 'Doe',
        dateOfBirth: '1985-01-01',
        gender: 'Male',
        email: 'john.doe@example.com',
        phone: '123-456-7890',
        address: '123 Main St',
        aboutInfo: 'Experienced software engineer.'
    },
    {
        id: 2,
        firstName: 'Jane',
        lastName: 'Smith',
        dateOfBirth: '1990-02-14',
        gender: 'Female',
        email: 'jane.smith@example.com',
        phone: '234-567-8901',
        address: '456 Elm St',
        aboutInfo: 'Marketing specialist with 5 years of experience.'
    },
    {
        id: 3,
        firstName: 'Alice',
        lastName: 'Johnson',
        dateOfBirth: '1978-03-22',
        gender: 'Female',
        email: 'alice.johnson@example.com',
        phone: '345-678-9012',
        address: '789 Oak St',
        aboutInfo: 'HR manager focused on employee relations.'
    },
    {
        id: 4,
        firstName: 'Bob',
        lastName: 'Brown',
        dateOfBirth: '1982-04-10',
        gender: 'Male',
        email: 'bob.brown@example.com',
        phone: '456-789-0123',
        address: '101 Pine St',
        aboutInfo: 'Financial analyst with expertise in budgeting.'
    },
    {
        id: 5,
        firstName: 'Charlie',
        lastName: 'Davis',
        dateOfBirth: '1995-05-30',
        gender: 'Male',
        email: 'charlie.davis@example.com',
        phone: '567-890-1234',
        address: '202 Maple St',
        aboutInfo: 'Junior web developer with a passion for coding.'
    },
    {
        id: 6,
        firstName: 'Diana',
        lastName: 'Wilson',
        dateOfBirth: '1987-06-18',
        gender: 'Female',
        email: 'diana.wilson@example.com',
        phone: '678-901-2345',
        address: '303 Birch St',
        aboutInfo: 'Project manager experienced in agile methodologies.'
    },
    {
        id: 7,
        firstName: 'Edward',
        lastName: 'Martinez',
        dateOfBirth: '1992-07-24',
        gender: 'Male',
        email: 'edward.martinez@example.com',
        phone: '789-012-3456',
        address: '404 Cedar St',
        aboutInfo: 'Creative graphic designer with a keen eye for detail.'
    },
    {
        id: 8,
        firstName: 'Fiona',
        lastName: 'Garcia',
        dateOfBirth: '1989-08-15',
        gender: 'Female',
        email: 'fiona.garcia@example.com',
        phone: '890-123-4567',
        address: '505 Walnut St',
        aboutInfo: 'Experienced data scientist with a background in statistics.'
    },
    {
        id: 9,
        firstName: 'George',
        lastName: 'Lee',
        dateOfBirth: '1983-09-05',
        gender: 'Male',
        email: 'george.lee@example.com',
        phone: '901-234-5678',
        address: '606 Cherry St',
        aboutInfo: 'Business analyst with a focus on process improvement.'
    },
    {
        id: 10,
        firstName: 'Helen',
        lastName: 'Taylor',
        dateOfBirth: '1991-10-29',
        gender: 'Female',
        email: 'helen.taylor@example.com',
        phone: '012-345-6789',
        address: '707 Aspen St',
        aboutInfo: 'UX/UI designer with a passion for user-centered design.'
    }

];

const CandidateList = () => {
    const [candidates, setCandidates] = useState(initialCandidates);
    const [search, setSearch] = useState('');
    const [sortBy, setSortBy] = useState('');
    const [sortOrder, setSortOrder] = useState('asc');
    const [tab, setTab] = useState(0);
    const [dialogOpen, setDialogOpen] = useState(false);
    const [editDialogOpen, setEditDialogOpen] = useState(false);
    const [newCandidate, setNewCandidate] = useState({
        firstName: '',
        lastName: '',
        dateOfBirth: '',
        gender: '',
        email: '',
        phone: '',
        address: '',
        aboutInfo: ''
    });
    const [editingCandidate, setEditingCandidate] = useState(null);

    const handleSearchChange = (event) => {
        setSearch(event.target.value);
    };

    const handleTabChange = (event, newValue) => {
        setTab(newValue);
    };

    const handleSort = (property) => {
        const isAsc = sortBy === property && sortOrder === 'asc';
        setSortOrder(isAsc ? 'desc' : 'asc');
        setSortBy(property);
    };

    const handleOpenDialog = () => {
        setDialogOpen(true);
    };

    const handleCloseDialog = () => {
        setDialogOpen(false);
    };

    const handleOpenEditDialog = (candidate) => {
        setEditingCandidate(candidate);
        setNewCandidate(candidate);
        setEditDialogOpen(true);
    };

    const handleCloseEditDialog = () => {
        setEditDialogOpen(false);
    };

    const handleInputChange = (event) => {
        const { name, value } = event.target;
        setNewCandidate(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleGenderChange = (event) => {
        setNewCandidate(prev => ({
            ...prev,
            gender: event.target.value
        }));
    };

    const handleAddCandidate = () => {
        setCandidates(prev => [
            ...prev,
            { id: prev.length + 1, ...newCandidate }
        ]);
        setNewCandidate({
            firstName: '',
            lastName: '',
            dateOfBirth: '',
            gender: '',
            email: '',
            phone: '',
            address: '',
            aboutInfo: ''
        });
        handleCloseDialog();
    };

    const handleUpdateCandidate = () => {
        setCandidates(prev => prev.map(candidate =>
            candidate.id === editingCandidate.id ? newCandidate : candidate
        ));
        setEditingCandidate(null);
        setNewCandidate({
            firstName: '',
            lastName: '',
            dateOfBirth: '',
            gender: '',
            email: '',
            phone: '',
            address: '',
            aboutInfo: ''
        });
        handleCloseEditDialog();
    };

    const filteredCandidates = candidates
        .filter(candidate =>
            candidate.firstName.toLowerCase().includes(search.toLowerCase()) ||
            candidate.lastName.toLowerCase().includes(search.toLowerCase()) ||
            candidate.email.toLowerCase().includes(search.toLowerCase())
        )
        .filter(candidate => {
            if (tab === 1) return candidate.gender === 'Male';
            if (tab === 2) return candidate.gender === 'Female';
            return true;
        })
        .sort((a, b) => {
            if (sortBy === '') return 0;
            if (sortOrder === 'asc') {
                return a[sortBy] > b[sortBy] ? 1 : -1;
            } else {
                return a[sortBy] < b[sortBy] ? 1 : -1;
            }
        });

    return (
        <Card sx={{ width: '85%', marginTop: 4, marginX: "auto" }}>
            <Paper className="p-3 mb-3 d-flex justify-content-between align-items-center">
                <Tabs value={tab} onChange={handleTabChange} aria-label="candidate tabs">
                    <Tab label="All Candidates" />
                    <Tab label="Males" />
                    <Tab label="Females" />
                </Tabs>
                <SearchBar handleSearchChange={handleSearchChange} search={search} />
                <IconButton color="primary" onClick={handleOpenDialog}>
                    <AddIcon />
                </IconButton>
            </Paper>

            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell></TableCell>
                            <TableCell>
                                <TableSortLabel
                                    active={sortBy === 'firstName'}
                                    direction={sortOrder}
                                    onClick={() => handleSort('firstName')}
                                >
                                    Name
                                </TableSortLabel>
                            </TableCell>
                            <TableCell>
                                <TableSortLabel
                                    active={sortBy === 'email'}
                                    direction={sortOrder}
                                    onClick={() => handleSort('email')}
                                >
                                    Email
                                </TableSortLabel>
                            </TableCell>
                            <TableCell>
                                <TableSortLabel
                                    active={sortBy === 'phone'}
                                    direction={sortOrder}
                                    onClick={() => handleSort('phone')}
                                >
                                    Phone
                                </TableSortLabel>
                            </TableCell>
                            <TableCell>
                                <TableSortLabel
                                    active={sortBy === 'dateOfBirth'}
                                    direction={sortOrder}
                                    onClick={() => handleSort('dateOfBirth')}
                                >
                                    Date of Birth
                                </TableSortLabel>
                            </TableCell>
                            <TableCell>Status</TableCell>
                            <TableCell>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {filteredCandidates.map(candidate => (
                            <TableRow key={candidate.id}>
                                <TableCell>
                                    <Avatar>{candidate.firstName[0]}</Avatar>
                                </TableCell>
                                <TableCell>
                                    <div>{candidate.firstName} {candidate.lastName}</div>
                                    <div>{candidate.email}</div>
                                </TableCell>
                                <TableCell>{candidate.email}</TableCell>
                                <TableCell>{candidate.phone}</TableCell>
                                <TableCell>{candidate.dateOfBirth}</TableCell>
                                <TableCell>
                                    <Chip label="Active" variant="outlined"/>
                                </TableCell>
                                <TableCell>
                                    <IconButton color="primary" onClick={() => handleOpenEditDialog(candidate)}>
                                        <EditIcon />
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>

            {/* Диалоговое окно для добавления кандидата */}
            <Dialog open={dialogOpen} onClose={handleCloseDialog}>
                <DialogTitle>Add New Candidate</DialogTitle>
                <DialogContent>
                    <TextField
                        margin="dense"
                        label="First Name"
                        type="text"
                        fullWidth
                        name="firstName"
                        value={newCandidate.firstName}
                        onChange={handleInputChange}
                    />
                    <TextField
                        margin="dense"
                        label="Last Name"
                        type="text"
                        fullWidth
                        name="lastName"
                        value={newCandidate.lastName}
                        onChange={handleInputChange}
                    />
                    <TextField
                        margin="dense"
                        label="Date of Birth"
                        type="date"
                        fullWidth
                        name="dateOfBirth"
                        value={newCandidate.dateOfBirth}
                        onChange={handleInputChange}
                        InputLabelProps={{ shrink: true }}
                    />
                    <FormControl fullWidth margin="dense">
                        <InputLabel>Gender</InputLabel>
                        <Select
                            value={newCandidate.gender}
                            onChange={handleGenderChange}
                            name="gender"
                        >
                            <MenuItem value="Male">Male</MenuItem>
                            <MenuItem value="Female">Female</MenuItem>
                        </Select>
                    </FormControl>
                    <TextField
                        margin="dense"
                        label="Email"
                        type="email"
                        fullWidth
                        name="email"
                        value={newCandidate.email}
                        onChange={handleInputChange}
                    />
                    <TextField
                        margin="dense"
                        label="Phone"
                        type="text"
                        fullWidth
                        name="phone"
                        value={newCandidate.phone}
                        onChange={handleInputChange}
                    />
                    <TextField
                        margin="dense"
                        label="Address"
                        type="text"
                        fullWidth
                        name="address"
                        value={newCandidate.address}
                        onChange={handleInputChange}
                    />
                    <TextField
                        margin="dense"
                        label="About Info"
                        type="text"
                        fullWidth
                        name="aboutInfo"
                        value={newCandidate.aboutInfo}
                        onChange={handleInputChange}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleCloseDialog} color="primary">
                        Cancel
                    </Button>
                    <Button onClick={handleAddCandidate} color="primary">
                        Add
                    </Button>
                </DialogActions>
            </Dialog>

            {/* Диалоговое окно для редактирования кандидата */}
            <Dialog open={editDialogOpen} onClose={handleCloseEditDialog}>
                <DialogTitle>Edit Candidate</DialogTitle>
                <DialogContent>
                    <TextField
                        margin="dense"
                        label="First Name"
                        type="text"
                        fullWidth
                        name="firstName"
                        value={newCandidate.firstName}
                        onChange={handleInputChange}
                    />
                    <TextField
                        margin="dense"
                        label="Last Name"
                        type="text"
                        fullWidth
                        name="lastName"
                        value={newCandidate.lastName}
                        onChange={handleInputChange}
                    />
                    <TextField
                        margin="dense"
                        label="Date of Birth"
                        type="date"
                        fullWidth
                        name="dateOfBirth"
                        value={newCandidate.dateOfBirth}
                        onChange={handleInputChange}
                        InputLabelProps={{ shrink: true }}
                    />
                    <FormControl fullWidth margin="dense">
                        <InputLabel>Gender</InputLabel>
                        <Select
                            value={newCandidate.gender}
                            onChange={handleGenderChange}
                            name="gender"
                        >
                            <MenuItem value="Male">Male</MenuItem>
                            <MenuItem value="Female">Female</MenuItem>
                        </Select>
                    </FormControl>
                    <TextField
                        margin="dense"
                        label="Email"
                        type="email"
                        fullWidth
                        name="email"
                        value={newCandidate.email}
                        onChange={handleInputChange}
                    />
                    <TextField
                        margin="dense"
                        label="Phone"
                        type="text"
                        fullWidth
                        name="phone"
                        value={newCandidate.phone}
                        onChange={handleInputChange}
                    />
                    <TextField
                        margin="dense"
                        label="Address"
                        type="text"
                        fullWidth
                        name="address"
                        value={newCandidate.address}
                        onChange={handleInputChange}
                    />
                    <TextField
                        margin="dense"
                        label="About Info"
                        type="text"
                        fullWidth
                        name="aboutInfo"
                        value={newCandidate.aboutInfo}
                        onChange={handleInputChange}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleCloseEditDialog} color="primary">
                        Cancel
                    </Button>
                    <Button onClick={handleUpdateCandidate} color="primary">
                        Update
                    </Button>
                </DialogActions>
            </Dialog>
        </Card>
    );
};

export default CandidateList;
