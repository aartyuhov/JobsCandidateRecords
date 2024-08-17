import React, {useEffect, useState} from 'react';
import {List, ListItem, ListItemText, Divider, Grid, Paper, Box, IconButton, CircularProgress} from '@mui/material';
import 'bootstrap/dist/css/bootstrap.min.css';
import CommentsSection from "../small-components/CommentsSection";
import {SpeakerNotes} from "@mui/icons-material";
import {useParams} from "react-router-dom";
import api from "../../services/api";

const ApplicationPage = () => {
    const { id } = useParams();
    const [applications, setApplications] = useState([]);
    const [comments, setComments] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [candidate, setCandidate] = useState({
        id: 0,
        firstName: "",
        lastName: ""
    });
    const [employeeId, setEmployeeId] = useState([]);

    useEffect(() =>
    {
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
    },[id]);


    return (
        <Box className="mt-4">
            <Grid container spacing={2}>
                <Grid item xs={6}>
                    { loading ? (
                        <div className={"d-flex align-content-center"}>
                            <CircularProgress color="inherit" />
                        </div>
                    ) : error ? (
                        <h4 className={"text-center"}>{error}</h4>
                    ) : (<Paper elevation={3} className="p-3">
                                <h3 className="text-center">Applications List for {candidate.lastName + ' ' + candidate.firstName}</h3>
                                <List>
                                    {applications.map((app) => (
                                        <React.Fragment key={app.applicationId}>
                                            <ListItem className="d-flex justify-content-between">
                                                <ListItemText primary={app.requestName} secondary={`Status: ${app.applicationStatus}`} />
                                                <IconButton color="info">
                                                    <SpeakerNotes />
                                                </IconButton>
                                            </ListItem>
                                            <Divider />
                                        </React.Fragment>
                                    ))}
                                </List>
                            </Paper>)}
                </Grid>
                <Grid item xs={6}>
                    <Paper elevation={3} className="p-3" style={{ height: '80vh' }}>
                        <CommentsSection comments={comments} setComments={setComments} />
                    </Paper>
                </Grid>
            </Grid>
        </Box>
    );
};

export default ApplicationPage;
