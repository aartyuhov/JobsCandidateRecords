import React, { useState, useEffect } from "react";
import {
    TextField,
    Button,
    Paper,
    Box,
    Typography,
    InputLabel,
    Select,
    MenuItem,
    FormControl,
    CircularProgress
} from "@mui/material";
import BasicDatePicker from "../small-components/BasicDatePicker";
import { ArrowBack } from "@mui/icons-material";
import { useNavigate, useParams } from "react-router-dom";
import api from "../../services/api";

function CandidateProfile() {
    const navigate = useNavigate();
    const { id } = useParams();
    const isEditMode = Boolean(id);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const [candidate, setCandidate] = useState({
        id: 0,
        firstName: "",
        lastName: "",
        dateOfBirth: "",
        gender: "",
        email: "",
        phone: "",
        address: "",
        aboutInfo: "",
        applicationStatusDTOs: null
    });

    useEffect(() => {
        if (isEditMode) {
            const fetchData = async (id) => {
                try {
                    const fetchedCandidate = await api.get(`/api/CandidatesDTO/${id}`);
                    setCandidate(fetchedCandidate.data);
                    setLoading(false);
                } catch (error) {
                    console.error('Error fetching data:', error);
                    setError('An error occurred while fetching the data. Please try again later.');
                    setLoading(false);
                }
            };
            fetchData(id).catch(error => console.error(error));
        }
        else
        {
            setLoading(false);
        }
    }, [id, isEditMode]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setCandidate({ ...candidate, [name]: value });
    };

    const handleSave = async (event) => {
        event.preventDefault();
        if (isEditMode) {
            try {
                candidate.dateOfBirth = new Date(event.target.dateOfBirth.value).toJSON().split('T')[0];
                candidate.gender = event.target.gender.value;
                const response = await api.put(`/api/CandidatesDTO`, candidate);
                if(response.status === 200)
                    navigate(-1);
                console.log("Updating candidate:", candidate);
            } catch (error) {
                console.error('Error fetching data:', error);
                setError('Error to put candidate!');
            }
            console.log("Updating candidate:", candidate);
        } else {
            try {
                candidate.dateOfBirth = new Date(event.target.dateOfBirth.value).toJSON().split('T')[0];
                candidate.male = event.target.gender.value;
                const response = await api.post(`/api/CandidatesDTO`, candidate);
                if(response.status === 201)
                    navigate(-1);
                console.log("Creating candidate:", candidate);
            } catch (error) {
                console.error('Error fetching data:', error);
                setError('Error to post candidate!');
            }
        }
    };

    return (
        <>
            <div className="float-start">
                <Button variant="outlined" onClick={() => navigate(-1)}><ArrowBack />Back</Button>
            </div>
            { loading ? (
                <div className={"d-flex align-content-center"}>
                    <CircularProgress color="inherit" />
                </div>
            ) : error ? (
                <h4 className={"text-center"}>{error}</h4>
            ) : (<Box
                    sx={{
                        display: "flex",
                        justifyContent: "center"
                    }}
                >
                    <Paper className="w-75" sx={{ padding: "20px", backgroundColor: "#ffffff" }}>
                        <Typography variant="h4" sx={{ marginBottom: "15px" }}>
                            {isEditMode ? "Edit Candidate Profile" : "Create Candidate Profile"}
                        </Typography>
                        <Box component="form"
                             onSubmit={handleSave}
                             sx={{
                                 "& .MuiTextField-root": { marginBottom: "15px" },
                             }}>
                            <TextField
                                label="First Name"
                                fullWidth
                                name="firstName"
                                value={candidate.firstName}
                                onChange={handleChange}
                                variant="outlined"
                                required
                            />
                            <TextField
                                label="Last Name"
                                fullWidth
                                name="lastName"
                                value={candidate.lastName}
                                onChange={handleChange}
                                variant="outlined"
                                required
                            />
                            <BasicDatePicker
                                label="Date of Birth"
                                name="dateOfBirth"
                                value={candidate.dateOfBirth}
                                className="col-md-12"
                                required
                            />
                            <FormControl variant="outlined" fullWidth required sx={{ marginBottom: "1em" }}>
                                <InputLabel>Gender</InputLabel>
                                <Select label="Gender" name="gender" defaultValue={candidate.gender}>
                                    <MenuItem key="Male" value="Male">Male</MenuItem>
                                    <MenuItem key="Female" value="Female">Female</MenuItem>
                                </Select>
                            </FormControl>
                            <TextField
                                label="Email"
                                fullWidth
                                name="email"
                                value={candidate.email}
                                onChange={handleChange}
                                variant="outlined"
                            />
                            <TextField
                                label="Phone"
                                fullWidth
                                name="phone"
                                value={candidate.phone}
                                onChange={handleChange}
                                variant="outlined"
                            />
                            <TextField
                                label="Address"
                                fullWidth
                                name="address"
                                value={candidate.address}
                                onChange={handleChange}
                                variant="outlined"
                            />
                            <TextField
                                label="About"
                                fullWidth
                                name="aboutInfo"
                                multiline
                                rows={4}
                                value={candidate.aboutInfo}
                                onChange={handleChange}
                                variant="outlined"
                            />
                            <Button
                                variant="contained"
                                color="primary"
                                type="submit"
                                sx={{ marginTop: "10px" }}>
                                {isEditMode ? "Update" : "Create"}
                            </Button>
                        </Box>
                    </Paper>
                </Box>)}
        </>
    );
}

export default CandidateProfile;
