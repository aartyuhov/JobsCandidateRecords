import React, { useEffect, useState } from 'react';
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
    Card,
    Stack,
    Divider,
    CircularProgress,
    Modal,
    Box,
    Typography,
    TextField,
    Button,
    MenuItem,
    Select,
    FormControl,
    InputLabel
} from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import 'bootstrap/dist/css/bootstrap.min.css';
import SearchBar from "../small-components/SearchBar";
import { useNavigate } from "react-router-dom";
import api from "../../services/api";
import { AddCircle, ListAlt } from "@mui/icons-material";

const CandidateList = () => {
    const navigate = useNavigate();
    const [employeeId, setEmployeeId] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [candidates, setCandidates] = useState([]);
    const [search, setSearch] = useState('');
    const [sortBy, setSortBy] = useState('');
    const [sortOrder, setSortOrder] = useState('asc');
    const [tab, setTab] = useState(0);

    const [open, setOpen] = useState(false); // Состояние для модального окна
    const [details, setDetails] = useState(''); // Состояние для хранения введенных данных
    const [positions, setPositions] = useState([]); // Состояние для хранения позиций
    const [vacancies, setVacancies] = useState([]); // Состояние для хранения вакансий
    const [selectedPosition, setSelectedPosition] = useState(''); // Состояние для выбранной позиции
    const [selectedPositionModal, setSelectedPositionModal] = useState(''); // Состояние для выбранной позиции
    const [selectedVacancy, setSelectedVacancy] = useState(''); // Состояние для выбранной вакансии
    const [selectedCandidate, setSelectedCandidate] = useState(null); // Состояние для выбранного кандидата

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

        const fetchPositions = async () => {
            try {
                const positionsResponse = await api.get('/api/Position');
                setPositions(positionsResponse.data);
            } catch (error) {
                console.error('Error fetching positions:', error);
            }
        };

        fetchPositions();
    }, []);

    const fetchVacancies = async (positionId) => {
        try {
            const vacanciesResponse = await api.get(`/api/RequestForEmployeeDTO/search?positionId=${positionId}`);
            setVacancies(vacanciesResponse.data);
        } catch (error) {
            console.error('Error fetching vacancies:', error);
        }
    };

    const fetchCandidates = async (positionId) => {
        try {
            const candidatesResponse = await api.get(`/api/CandidatesDTO/position/${positionId}`);
            setCandidates(candidatesResponse.data);
        } catch (error) {
            console.error('Error fetching candidates:', error);
        }
    };

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

    const handleOpen = (candidateId) => {
        setSelectedCandidate(candidateId);
        setOpen(true);
    };

    const handleClose = () => setOpen(false);

    const handleApply = async () => {
        console.log("Selected Candidate:", selectedCandidate);
        console.log("Employee ID:", employeeId);
        console.log("Selected Vacancy:", selectedVacancy);

        if (!selectedCandidate || !employeeId || !selectedVacancy) {
            console.error('Required fields are missing');
            return;
        }

        const applicationData = {
            candidateId: selectedCandidate,
            employeeWhoCreatedId: employeeId,
            creationDate: new Date().toISOString(),
            details
        };

        try {
            await api.post(`/api/Application?requestForEmployeeId=${selectedVacancy}`, applicationData);
            handleClose();
        } catch (error) {
            console.error('Error submitting application:', error);
            setError('An error occurred while submitting the application. Please try again later.');
        }
    };

    const handlePositionChange = (event) => {
        const positionId = event.target.value;
        setSelectedPosition(positionId);
        fetchCandidates(positionId); // Подгружаем вакансии при изменении позиции
    };

    const handlePositionInModalChange = (event) => {
        const positionId = event.target.value;
        setSelectedPositionModal(positionId);
        fetchVacancies(positionId); // Подгружаем вакансии при изменении позиции
    };

    const handleVacancyChange = (event) => {
        setSelectedVacancy(event.target.value);
    };

    const handleDetailsChange = (event) => {
        setDetails(event.target.value);
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
                {/* Список позиций */}
                <FormControl sx={{ mb: 2, width: "25%" }}>
                    <InputLabel id="positions-select-label">Position</InputLabel>
                    <Select
                        labelId="positions-select-label"
                        value={selectedPosition}
                        onChange={handlePositionChange}
                        label="Position">
                        {positions.map(position => (
                            <MenuItem key={position.id} value={position.id}>
                                {position.title}
                            </MenuItem>
                        ))}
                    </Select>
                </FormControl>

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
                                            <IconButton color="primary" onClick={() => handleOpen(candidate.id)}>
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
                        height: 550, // Увеличенная высота окна
                    }}
                >
                    <Typography id="modal-modal-title" variant="h6" component="h2" sx={{ mb: 2 }}>
                        Add new application
                    </Typography>

                    {/* Список позиций */}
                    <FormControl fullWidth sx={{ mb: 2 }}>
                        <InputLabel id="positions-select-label">Position</InputLabel>
                        <Select
                            labelId="positions-select-label"
                            value={selectedPositionModal}
                            onChange={handlePositionInModalChange}
                            label="Position"
                        >
                            {positions.map(position => (
                                <MenuItem key={position.id} value={position.id}>
                                    {position.title}
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>

                    {/* Список вакансий */}
                    <FormControl fullWidth sx={{ mb: 2 }}>
                        <InputLabel id="vacancies-select-label">Vacancy</InputLabel>
                        <Select
                            labelId="vacancies-select-label"
                            value={selectedVacancy}
                            onChange={handleVacancyChange}
                            label="Vacancy"
                        >
                            {vacancies.map(vacancy => (
                                <MenuItem key={vacancy.id} value={vacancy.id}>
                                    {vacancy.name}
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>

                    <TextField
                        label="Description"
                        fullWidth
                        multiline
                        rows={8} // Количество строк для текстового поля
                        variant="outlined"
                        value={details}
                        onChange={handleDetailsChange} // Обработчик изменений
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
