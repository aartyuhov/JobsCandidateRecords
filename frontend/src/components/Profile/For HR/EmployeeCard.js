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
    CircularProgress, OutlinedInput, InputAdornment, IconButton, Chip
} from '@mui/material';
import 'bootstrap/dist/css/bootstrap.min.css';
import {ArrowBack, Visibility, VisibilityOff} from "@mui/icons-material";
import {useNavigate} from "react-router-dom";
import BasicDatePicker from "../../small-components/BasicDatePicker";
import React, {useEffect, useState} from "react";
import api from "../../../services/api";

const PositionList = () => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await api.get(`/api/Position`);
                setData(response.data);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
                setError('An error occurred while fetching the data. Please try again later.');
                setLoading(false);
            }
        };

        fetchData().catch(error => console.log(error));
    }, []);
    return (
        <>
            { loading ? (
                <div className={"d-flex align-content-center"}>
                    <CircularProgress color="inherit" />
                </div>
            ) : error ? (
                    <h4 className={"text-center"}>{error}</h4>
                ) :
                (
                    <FormControl variant="outlined" fullWidth required>
                        <InputLabel>Position</InputLabel>
                        <Select label="Position" name="position" defaultValue="">
                            {data.map(item =>
                                <MenuItem key={item.id} value={item.id}>{item.title}</MenuItem>
                            )}
                        </Select>
                    </FormControl>
                )
            }
        </>
    );
};

const RolesList = () => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await api.get(`/api/Roles`);
                setData(response.data);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
                setError('An error occurred while fetching the data. Please try again later.');
                setLoading(false);
            }
        };

        fetchData().catch(error => console.log(error));
    }, []);
    return (
        <>
            { loading ? (
                <div className={"d-flex align-content-center"}>
                    <CircularProgress color="inherit" />
                </div>
            ) : error ? (
                    <h4 className={"text-center"}>{error}</h4>
                ) :
                (
                    <FormControl variant="outlined" fullWidth required>
                        <InputLabel>Role</InputLabel>
                        <Select label="Role" name="role" defaultValue="">
                            {data.map(item =>
                                <MenuItem key={item.name} value={item.name}>{item.name}</MenuItem>
                            )}
                        </Select>
                    </FormControl>
                )
            }
        </>
    );
};

const EmployeeCard = () => {
    const navigate = useNavigate();
    const [showPassword, setShowPassword] = useState(false);
    const [errorText, setErrorText] = useState(null);
    const handleClickShowPassword = () => setShowPassword((show) => !show);

    const SubmitHandler = async (event) =>
    {
        event.preventDefault();
        setErrorText(null);
        try
        {
            const responseUser = await api.post('/api/Users', {
                "username" : event.target.username.value,
                "email" : event.target.userEmail.value,
                "password" : event.target.password.value,
                "confirmPassword" : event.target.confirmPassword.value
            });
            if(responseUser.status === 200)
            {
                const createdUser = await  api.get('/api/Users/user/email/' + event.target.userEmail.value);
                if(createdUser.status === 200)
                {
                    await api.post(`/api/Roles/${createdUser.data.id}/assign/${event.target.role.value}`);
                    const responseEmployee = await api.post('/api/Employees', {
                        "id": 0,
                        "firstName": event.target.firstName.value,
                        "lastName": event.target.lastName.value,
                        "dateOfBirth": new Date(event.target.birthDay.value).toJSON().split('T')[0],
                        "gender": event.target.gender.value,
                        "email": event.target.email.value,
                        "phoneNumber": event.target.phoneNumber.value,
                        "address": event.target.address.value,
                        "hireDate": new Date(event.target.hireDate.value).toJSON(),
                        "positionId": event.target.position.value,
                    });
                    if(responseEmployee.status === 201)
                    {
                        await api.post(`/api/Users/${createdUser.data.id}/assign/${responseEmployee.data.id}`, {});
                        navigate(-1);
                    }
                }
            }


        }
        catch (e)
        {
            console.error(e.message);
            if(e.response.status === 400 && e.response.data[0].description)
            {
                setErrorText('WARNING ' + e.response.data[0].description);
            }
        }
    };

    return (
        <>
            <div className="float-start">
                <Button variant="outlined" onClick={() => navigate(-1)}><ArrowBack/>Back</Button>
            </div>
            <div className="container my-5">
                <Card className="p-4 bg-light text-dark rounded-lg shadow-md">
                    <CardContent>
                        <Typography variant="h4" component="h1" className="font-weight-bold mb-4">Create employee</Typography>
                        <form onSubmit={SubmitHandler}>
                            {/* Account information */}
                            <div className="mb-4">
                                <Typography variant="h5" component="h2" className="font-weight-bold mb-3">Account
                                    information</Typography>
                                <div className="d-flex align-items-center mb-4">
                                    <Avatar alt="avatar" src="https://placehold.co/100x100?text=🖼️"
                                            className="border border-dashed border-secondary mr-3"
                                            style={{width: '100px', height: '100px'}}/>
                                    <div>
                                        <Button variant="contained" color="primary"
                                                className="mr-2 mx-4">Select</Button>
                                        <Typography variant="body2" className="text-muted mt-1 mx-3">Min 400x400px, PNG
                                            or JPG</Typography>
                                    </div>
                                </div>
                                <div className="form-row ">
                                    <div className="form-group col-md-12 my-3 d-flex align-content-between gap-md-2">
                                        <TextField label="First name" name="firstName" variant="outlined" fullWidth
                                                   required/>
                                        <TextField label="Last name" name="lastName" variant="outlined" fullWidth
                                                   required/>
                                    </div>
                                    <div className="form-group col-md-12 my-3 d-flex align-content-between gap-md-2">
                                        <TextField label="Email" name="email" type="email" variant="outlined" fullWidth
                                                   required/>
                                        <TextField label="Phone number" type="tel" name="phoneNumber" variant="outlined"
                                                   fullWidth/>
                                    </div>
                                </div>
                                <div className="form-row col-md-12 my-3 d-flex align-content-between gap-md-2">
                                    <div className="form-group col-md-6">
                                        <PositionList/>
                                    </div>
                                    <div className="form-group col-md-6">
                                        <FormControl variant="outlined" fullWidth required>
                                            <InputLabel>Gender</InputLabel>
                                            <Select label="Gender" name="gender" defaultValue="">
                                                <MenuItem value="Male">Male</MenuItem>
                                                <MenuItem value="Female">Female</MenuItem>
                                            </Select>
                                        </FormControl>
                                    </div>
                                </div>
                                <div className="form-row">
                                    <BasicDatePicker label="Hire date" name="hireDate" className="col-md-6" required/>
                                </div>
                            </div>
                            <div className="mb-4">
                                <Typography variant="h5" component="h2" className="font-weight-bold mb-3">Personal
                                    information</Typography>
                                <div className="form-row mb-2">
                                    <div className="form-group col-md-6">
                                        <TextField label="Address" name="address" variant="outlined" fullWidth/>
                                    </div>
                                </div>
                                <div className="form-row">
                                    <div className="form-group col-md-12">
                                        <BasicDatePicker label="Birthday" name="birthDay" className="col-md-6"
                                                         required/>
                                    </div>
                                </div>
                            </div>

                            <div className="mb-4">
                                <Typography variant="h5" component="h2" className="font-weight-bold mb-3">User
                                    information</Typography>
                                <div className="form-row mb-2">
                                    <div className="form-group col-md-12 my-3 d-flex align-content-between gap-md-2">
                                        <TextField label="Username" name="username" variant="outlined" fullWidth
                                                   required/>
                                        <TextField label="Email for user" name="userEmail" type="email" variant="outlined" fullWidth
                                                   required/>
                                    </div>
                                </div>
                                <div className="form-row">

                                    { errorText ? (<Chip label={errorText} className="mb-3" color="warning" size="small" />) : ""}

                                    <div className="form-group col-md-6">
                                        <div className="d-flex flex-lg-column flex-md-row mb-3">
                                            <FormControl variant="outlined">
                                                <InputLabel htmlFor="password">Password</InputLabel>
                                                <OutlinedInput
                                                    id="password"
                                                    name="password"
                                                    type={showPassword ? 'text' : 'password'}
                                                    endAdornment={
                                                        <InputAdornment position="end">
                                                            <IconButton
                                                                aria-label="toggle password visibility"
                                                                onClick={handleClickShowPassword}
                                                                edge="end"
                                                            >
                                                                {showPassword ? <VisibilityOff/> : <Visibility/>}
                                                            </IconButton>
                                                        </InputAdornment>
                                                    }
                                                    label="Password"
                                                />
                                            </FormControl>
                                        </div>
                                        <div className="d-flex flex-lg-column flex-md-row mb-3">
                                            <FormControl variant="outlined">
                                                <InputLabel htmlFor="confirmPassword">Confirm password</InputLabel>
                                                <OutlinedInput
                                                    id="confirmPassword"
                                                    name="confirmPassword"
                                                    type={showPassword ? 'text' : 'password'}
                                                    endAdornment={
                                                        <InputAdornment position="end">
                                                            <IconButton
                                                                aria-label="toggle password visibility"
                                                                onClick={handleClickShowPassword}
                                                                edge="end"
                                                            >
                                                                {showPassword ? <VisibilityOff/> : <Visibility/>}
                                                            </IconButton>
                                                        </InputAdornment>
                                                    }
                                                    label="Confirm password"
                                                />
                                            </FormControl>
                                        </div>
                                    </div>
                                    <div className="form-group col-md-6">
                                        <RolesList/>
                                    </div>
                                </div>
                            </div>
                            {/* Form actions */}
                            <div className="d-flex justify-content-end gap-md-2">
                                <Button variant="contained" color="primary" type="submit">Create</Button>
                            </div>
                        </form>
                    </CardContent>
                </Card>
            </div>
        </>
    );
};

export default EmployeeCard;
