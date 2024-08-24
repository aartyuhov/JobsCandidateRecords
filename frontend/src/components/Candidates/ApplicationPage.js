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
    Chip,
    Button
} from '@mui/material';
import 'bootstrap/dist/css/bootstrap.min.css';
import CommentsSection from "../small-components/CommentsSection";
import { SpeakerNotes } from "@mui/icons-material";
import { useParams } from "react-router-dom";
import api from "../../services/api";

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

    useEffect(() => {
        const fetchData = async (id) => {
            try {
                const fetchedCandidate = await api.get(`/api/CandidatesDTO/${id}`);
                setCandidate(fetchedCandidate.data);
                setApplications(fetchedCandidate.data.applicationStatusDTOs);
                const fetchedEmployee = await api.get('/api/Users/get-tuple');
                setEmployeeId(fetchedEmployee.data.employeeDto.id);
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
            setLoading(false);
        } catch (error) {
            console.error('Error fetching data:', error);
            setError('An error occurred while fetching the data. Please try again later.');
            setLoading(false);
        }
    };

    const handleProgressClick = async (applicationId) => {
        try {
            const data = {
                applicationIds: [applicationId],
                newStatus: 1 // Assuming 1 represents 'In Process'
            };
            await api.put(`/api/RequestForEmployeeDTO/updateapplicationStatus`, data);
            setApplications(applications.map(app =>
                app.applicationId === applicationId ? { ...app, applicationStatus: 'InProcess' } : app
            ));
        } catch (error) {
            console.error('Error updating application status:', error);
        }
    };

    const handleCloseClick = async (applicationId) => {
        try {
            const data = {
                applicationIds: [applicationId],
                newStatus: 2 // Assuming 2 represents 'Closed'
            };
            await api.put(`/api/RequestForEmployeeDTO/updateapplicationStatus`, data);
            setApplications(applications.map(app =>
                app.applicationId === applicationId ? { ...app, applicationStatus: 'Closed' } : app
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
                        <Paper elevation={3} className="p-3">
                            <h3 className="text-center">Applications for {candidate.lastName + ' ' + candidate.firstName}</h3>
                            <List>
                                {applications.map((app) => (
                                    <React.Fragment key={app.applicationId}>
                                        <ListItem className="d-flex justify-content-between align-items-center">
                                            <ListItemText
                                                primary={app.requestName}
                                                secondary={`Status: ${app.applicationStatus}`}
                                            />
                                            <Box display="flex" alignItems="center" gap={1}>
                                                {app.applicationStatus === 'New' && (
                                                    <Chip
                                                        label="To progress"
                                                        variant="outlined"
                                                        onClick={() => handleProgressClick(app.applicationId)}
                                                        sx={{
                                                            color: 'yellow',
                                                            borderColor: 'yellow',
                                                            cursor: 'pointer'
                                                        }}
                                                    />
                                                )}
                                                {app.applicationStatus === 'InProcess' && (
                                                    <Button
                                                        variant="outlined"
                                                        color="error"
                                                        onClick={() => handleCloseClick(app.applicationId)}
                                                        sx={{ color: 'red', borderColor: 'red' }}
                                                    >
                                                        Closed
                                                    </Button>
                                                )}
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
