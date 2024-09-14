import React, { useEffect, useState } from 'react';
import {
    List,
    ListItem,
    ListItemText,
    Divider,
    Grid,
    Paper,
    Box,
    IconButton,
    CircularProgress,
    TextField,
    Typography,
    Select,
    MenuItem,
    FormControl,
    Button
} from '@mui/material';
import 'bootstrap/dist/css/bootstrap.min.css';
import CommentsSection from "../small-components/CommentsSection";
import { SpeakerNotes } from "@mui/icons-material";
import { useParams } from "react-router-dom";
import api from "../../services/api";
import { ApplicationStatus, UserFriendlyStatusLabels } from "../../constants/applicationstatus";
import StatusSection from "../small-components/StatusSection";
import Chip from "@mui/material/Chip";

const ApplicationPage = () => {
    const { id } = useParams();
    const [applications, setApplications] = useState([]);
    const [comments, setComments] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [selectedApplicationId, setSelectedApplicationId] = useState('');
    const [selectedApplicationName, setSelectedApplicationName] = useState(null);
    const [candidate, setCandidate] = useState({
        id: 0,
        firstName: "",
        lastName: ""
    });
    const [employeeId, setEmployeeId] = useState([]);
    const [selectedDescription, setSelectedDescription] = useState('');
    const [statuses, setStatuses] = useState([]);
    const [statusMap, setStatusMap] = useState({});

    useEffect(() => {
        const fetchData = async (id) => {
            try {
                const fetchedCandidate = await api.get(`/api/CandidatesDTO/${id}`);
                setCandidate(fetchedCandidate.data);

                const fetchedEmployee = await api.get('/api/Users/get-tuple');
                setEmployeeId(fetchedEmployee.data.employeeDto.id);

                const applications = fetchedCandidate.data.applicationStatusDTOs;

                // Инициализация статуса для каждого приложения, перевод из label в value
                const initialStatusMap = {};
                applications.forEach(app => {
                    const status = Object.values(ApplicationStatus).find(s => s.value === app.applicationStatus);
                    if (status) {
                        initialStatusMap[app.applicationId] = status.value;
                    }
                });
                setStatusMap(initialStatusMap);
                setApplications(applications);

                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
                setError('An error occurred while fetching the data. Please try again later.');
                setLoading(false);
            }
        };
        fetchData(id).catch(error => console.error(error));
    }, [id]);

    const handleIconClick = async (applicationId, requestName) => {
        try {
            const fetchedApplication = await api.get(`/api/Application/${applicationId}`);
            setSelectedDescription(fetchedApplication.data.details);
            setSelectedApplicationId(applicationId);
            setSelectedApplicationName(requestName);

            const fetchedNotes = await api.get(`/api/NotesDTO/application/${applicationId}`);
            setComments(fetchedNotes.data);

            const fetchedStatuses = await api.get(`/api/ApplicationStatusHistory/applicationId=${applicationId}`);
            setStatuses(fetchedStatuses.data);

            setLoading(false);
        } catch (error) {
            console.error('Error fetching data:', error);
            setError('An error occurred while fetching the data. Please try again later.');
            setLoading(false);
        }
    };

    const handleStatusChange = (applicationId, newStatusValue) => {
        setStatusMap(prevStatusMap => ({
            ...prevStatusMap,
            [applicationId]: newStatusValue
        }));
    };

    const handleStatusSubmit = async (applicationId) => {
        const newStatusValue = statusMap[applicationId];
        try {
            const data = {
                applicationId: applicationId,
                requestName:'',
                applicationStatus: newStatusValue
            };
            await api.post(`/api/ApplicationStatusHistory`, data);

            setApplications(applications.map(app =>
                app.applicationId === applicationId ? { ...app, applicationStatus: Object.keys(UserFriendlyStatusLabels).find(key => ApplicationStatus[key].value === newStatusValue) } : app
            ));
        } catch (error) {
            console.error('Error updating application status:', error);
        }
    };

    return (
        <Box className="mt-4">
            <Grid container spacing={2}>
                <Grid item xs={6}>
                    {loading ? (
                        <div className={"d-flex align-content-center"}>
                            <CircularProgress color="inherit" />
                        </div>
                    ) : error ? (
                        <h4 className={"text-center"}>{error}</h4>
                    ) : (
                        <>
                            <Paper elevation={3} className="p-3 mb-4">
                                <h3 className="text-center">Applications for {candidate.lastName + ' ' + candidate.firstName}</h3>
                                <List>
                                    {applications.map((app) => (
                                        <React.Fragment key={app.applicationId}>
                                            <ListItem className="d-flex justify-content-between align-items-center">
                                                <Box display="flex" gap={1}>
                                                    <ListItemText primary={app.requestName} />
                                                    <Chip
                                                        label={app.positionTitles}
                                                        variant="outlined"
                                                        sx={{
                                                            marginLeft: '10px',
                                                            color: 'white',
                                                            borderColor: '#800000',
                                                            alignSelf: 'flex-start'
                                                        }}
                                                    />
                                                </Box>
                                                <Box display="flex" alignItems="center" gap={1}>
                                                    <Box mr={2}>
                                                        <Typography variant="body2" color="textSecondary">
                                                            Status:
                                                        </Typography>
                                                    </Box>
                                                    <FormControl variant="outlined" size="small">
                                                        <Select
                                                            value={statusMap[app.applicationId]}
                                                            onChange={(e) => handleStatusChange(app.applicationId, e.target.value)}
                                                            displayEmpty
                                                        >
                                                            {Object.values(ApplicationStatus).map((status) => (
                                                                <MenuItem key={status.value} value={status.value}>
                                                                    {UserFriendlyStatusLabels[Object.keys(ApplicationStatus).find(key => ApplicationStatus[key].value === status.value)]}
                                                                </MenuItem>
                                                            ))}
                                                        </Select>
                                                    </FormControl>
                                                    <Button
                                                        variant="contained"
                                                        color="primary"
                                                        onClick={() => handleStatusSubmit(app.applicationId)}
                                                    >
                                                        Update Status
                                                    </Button>
                                                    <IconButton color="info" onClick={() => handleIconClick(app.applicationId, app.requestName)}>
                                                        <SpeakerNotes />
                                                    </IconButton>
                                                </Box>
                                            </ListItem>
                                            <Divider />
                                        </React.Fragment>
                                    ))}
                                </List>
                            </Paper>
                            <Paper elevation={3} className="p-3" style={{ height: '65vh' }}>
                                <StatusSection statuses={statuses}/>
                            </Paper>
                        </>
                    )}
                </Grid>
                <Grid item xs={6}>
                    <Paper elevation={3} className="p-3 mb-2" style={{ height: '7vh' }}>
                        <Typography id="modal-modal-title" variant="h5" component="h2" sx={{ mb: 2 }}>
                            Selected application: {selectedApplicationName ? selectedApplicationName : "None"}
                        </Typography>
                    </Paper>
                    <Paper elevation={3} className="p-3 mb-2" style={{ height: '25vh' }}>
                        <TextField
                            label="Description"
                            fullWidth
                            multiline
                            rows={8} // Количество строк для текстового поля
                            variant="outlined"
                            value={selectedDescription} // Устанавливаем выбранное описание в текстовое поле
                            sx={{ mb: 2 }}
                            InputProps={{
                                readOnly: true
                            }}
                        />
                    </Paper>
                    <Paper elevation={3} className="p-3" style={{ height: '70vh' }}>
                        <CommentsSection comments={comments} setComments={setComments} applicationId={selectedApplicationId} employeeId={employeeId} />
                    </Paper>
                </Grid>
            </Grid>
        </Box>
    );
};

export default ApplicationPage;
