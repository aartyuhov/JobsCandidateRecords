import {
    Button,
    TextField,
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    Card,
    CardContent,
    Typography,
    Avatar,
    CircularProgress
} from '@mui/material';
import 'bootstrap/dist/css/bootstrap.min.css';
import {ArrowBack} from "@mui/icons-material";
import {useNavigate, useParams} from "react-router-dom";
import BasicDatePicker from "../../small-components/BasicDatePicker";
import React, {useEffect, useState} from "react";
import PositionList from "../../small-components/PositionList";
import api from "../../../services/api";

const EmployeeEdit = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [employee, setEmployee] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [selectedPositionId, setSelectedPositionId] = useState('');

    useEffect(() => {
        const fetchEmployee = async () => {
            try {
                const response = await api.get(`/api/EmployeeDTO/${id}`);
                setEmployee(response.data);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching employee data:', error);
                setError('An error occurred while fetching the data. Please try again later.');
                setLoading(false);
            }
        };

        fetchEmployee();
    }, [id]);

    const SubmitHandler = async (event) => {
        event.preventDefault();
        try {
            const response = await api.put(`/api/EmployeeDTO`, {
                "id": id,
                "firstName": event.target.firstName.value,
                "lastName": event.target.lastName.value,
                "dateOfBirth": new Date(event.target.birthDay.value).toJSON().split('T')[0],
                "gender": event.target.gender.value,
                "email": event.target.email.value,
                "phoneNumber": event.target.phoneNumber.value,
                "address": event.target.address.value,
                "hireDate": new Date(event.target.hireDate.value).toJSON(),
                "positionId": selectedPositionId,
            });
            if(response.status === 200) {
                navigate(-1);
            }
        } catch (e) {
            console.error(e.message);
        }
    };

    const handlePositionChange = (positionId) => {
        setSelectedPositionId(positionId);
    };

    return (
        <>
            <div className="float-start">
                <Button variant="outlined" onClick={() => navigate(-1)}><ArrowBack/>Back</Button>
            </div>
            <div className="container my-5">
                {loading ? (
                    <div className="d-flex align-content-center">
                        <CircularProgress color="inherit" />
                    </div>
                ) : error ? (
                    <h4 className="text-center">{error}</h4>
                ) : (
                    <Card className="p-4 bg-light text-dark rounded-lg shadow-md">
                        <CardContent>
                            <Typography variant="h4" component="h1" className="font-weight-bold mb-4">Edit employee</Typography>
                            <form onSubmit={SubmitHandler}>
                                <div className="mb-4">
                                    <Typography variant="h5" component="h2" className="font-weight-bold mb-3">Account information</Typography>
                                    <div className="d-flex align-items-center mb-4">
                                        <Avatar alt="avatar" src={employee.avatarUrl ?? 'https://placehold.co/100x100?text=🖼️'} className="border border-dashed border-secondary mr-3" style={{width: '100px', height: '100px'}}/>
                                        <div>
                                            <Button variant="contained" color="primary" className="mr-2 mx-4">Select</Button>
                                            <Typography variant="body2" className="text-muted mt-1 mx-3">Min 400x400px, PNG or JPG</Typography>
                                        </div>
                                    </div>
                                    <div className="form-row">
                                        <div className="form-group col-md-12 my-3 d-flex align-content-between gap-md-2">
                                            <TextField label="First name" name="firstName" variant="outlined" defaultValue={employee.firstName} fullWidth required />
                                            <TextField label="Last name" name="lastName" variant="outlined" defaultValue={employee.lastName} fullWidth required />
                                        </div>
                                        <div className="form-group col-md-12 my-3 d-flex align-content-between gap-md-2">
                                            <TextField label="Email" name="email" type="email" variant="outlined" defaultValue={employee.email} fullWidth required />
                                            <TextField label="Phone number" type="tel" name="phoneNumber" variant="outlined" defaultValue={employee.phoneNumber} fullWidth />
                                        </div>
                                    </div>
                                    <div className="form-row col-md-12 my-3 d-flex align-content-between gap-md-2">
                                        <div className="form-group col-md-6">
                                            <PositionList selectedPositionId={employee.positionId} handlePositionChange={handlePositionChange} />
                                        </div>
                                        <div className="form-group col-md-6">
                                            <FormControl variant="outlined" fullWidth required>
                                                <InputLabel>Gender</InputLabel>
                                                <Select label="Gender" name="gender" defaultValue={employee.gender}>
                                                    <MenuItem value="Male">Male</MenuItem>
                                                    <MenuItem value="Female">Female</MenuItem>
                                                </Select>
                                            </FormControl>
                                        </div>
                                    </div>
                                    <div className="form-row">
                                        <BasicDatePicker label="Hire date" name="hireDate" defaultValue={employee.hireDate} value={employee.hireDate} className="col-md-6" required />
                                    </div>
                                </div>
                                <div className="mb-4">
                                    <Typography variant="h5" component="h2" className="font-weight-bold mb-3">Personal information</Typography>
                                    <div className="form-row">
                                        <div className="form-group col-md-6">
                                            <TextField label="Address" name="address" variant="outlined" defaultValue={employee.address} fullWidth />
                                        </div>
                                    </div>
                                    <div className="form-row">
                                        <div className="form-group col-md-12">
                                            <BasicDatePicker label="Birthday" name="birthDay" defaultValue={employee.dateOfBirth} value={employee.dateOfBirth} className="col-md-6" required />
                                        </div>
                                    </div>
                                </div>
                                <div className="d-flex justify-content-end gap-md-2">
                                    <Button variant="contained" color="primary" type="submit">Update</Button>
                                </div>
                            </form>
                        </CardContent>
                    </Card>
                )}
            </div>
        </>
    );
};

export default EmployeeEdit;
