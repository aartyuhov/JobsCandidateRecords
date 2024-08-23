import React, {useEffect, useState} from 'react';
import {
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
    IconButton,
    Card, Stack, Divider, CircularProgress, Modal, Box, Typography, TextField, Button
} from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import 'bootstrap/dist/css/bootstrap.min.css';
import SearchBar from "../small-components/SearchBar";
import {useNavigate} from "react-router-dom";
import api from "../../services/api";
import {AddCircle, ListAlt} from "@mui/icons-material";

const CandidateList = () => {
    const navigate = useNavigate();
    const [employeeId, setEmployeeId] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [candidates, setCandidates] = useState([]);
    const [search, setSearch] = useState('');
    const [sortBy, setSortBy] = useState('');
    const [sortOrder, setSortOrder] = useState('asc');
    const [tab, setTab] = useState(0);

    const [open, setOpen] = useState(false); // Состояние для модального окна
    const [details, setDetails] = useState(''); // Состояние для хранения введенных данных

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await api.get(`/api/CandidatesDTO`);
                setCandidates(response.data);
                const fetchedEmployee = await api.get('/api/Users/get-tuple');
                setEmployeeId(fetchedEmployee.data.employeeDto.id);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
                setError('An error occurred while fetching the data. Please try again later.');
                setLoading(false);
            }
        };

        fetchData();
    },[]);

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

    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const handleApply = () => {
        console.log('Details:', details);
        handleClose(); // Закрыть модальное окно после нажатия на "Apply"
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
                <IconButton color="primary" onClick={() => navigate(`/candidates/new`)}>
                    <AddIcon />
                </IconButton>
            </Paper>

            {loading ? (
                <div className={"d-flex align-content-center"}>
                    <CircularProgress color="inherit" />
                </div>
            ) : error ? (
                <h4 className={"text-center"}>{error}</h4>
            ) : (
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
                                        <Stack
                                            direction="row"
                                            divider={<Divider orientation="vertical" flexItem />}
                                            spacing={2}
                                        >
                                            <IconButton color="primary" onClick={handleOpen}>
                                                <AddCircle />
                                            </IconButton>
                                            <IconButton color="primary" onClick={() => navigate(`/candidates/${candidate.id}`)}>
                                                <EditIcon />
                                            </IconButton>
                                            <IconButton color="primary" onClick={() => navigate(`/application/${candidate.id}`)}>
                                                <ListAlt />
                                            </IconButton>
                                        </Stack>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}

            {/* Модальное окно */}
            <Modal
                open={open}
                onClose={handleClose}
                aria-labelledby="modal-modal-title"
                aria-describedby="modal-modal-description"
            >
                <Box
                    sx={{
                        position: 'absolute',
                        top: '50%',
                        left: '50%',
                        transform: 'translate(-50%, -50%)',
                        width: 600, // Увеличенная ширина окна
                        bgcolor: 'background.paper',
                        boxShadow: 24,
                        p: 4,
                        height: 400, // Увеличенная высота окна
                    }}
                >
                    <Typography id="modal-modal-title" variant="h6" component="h2" sx={{ mb: 2 }}>
                        Add new application
                    </Typography>
                    <TextField
                        label="Description"
                        fullWidth
                        multiline
                        rows={8} // Количество строк для текстового поля
                        variant="outlined"
                        value={details}
                        onChange={(e) => setDetails(e.target.value)}
                        sx={{ mb: 2 }}
                    />
                    <Button variant="contained" color="primary" onClick={handleApply}>
                        Apply
                    </Button>
                </Box>
            </Modal>
        </Card>
    );
};

export default CandidateList;
