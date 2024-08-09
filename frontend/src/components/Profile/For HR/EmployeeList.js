import React, {useEffect, useState} from 'react';
import {
    Button,
    Card,
    CardContent,
    Typography,
    Avatar,
    List,
    ListItem,
    ListItemAvatar,
    ListItemText,
    IconButton,
    CircularProgress
} from '@mui/material';
import 'bootstrap/dist/css/bootstrap.min.css';
import {Edit} from "@mui/icons-material";
import {Link} from "react-router-dom";
import { useNavigate } from 'react-router-dom';
import api from "../../../services/api";

const EmployeeList = () => {
    const [employees, setEmployees] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await api.get(`/api/EmployeeDTO`);
                setEmployees(response.data);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
                setError('An error occurred while fetching the data. Please try again later.');
                setLoading(false);
            }
        };

        fetchData();
    },[]);

    return (
        <div className="d-flex align-content-start justify-content-center min-vh-100 bg-light text-dark">
            <Card className="w-100 max-w-3xl shadow-lg rounded-lg">
                <CardContent>
                    <div className="d-flex justify-content-between align-items-center mb-4">
                        <Typography variant="h4" component="h1" className="fw-bold">Employee List</Typography>
                        <Button variant="contained" color="primary" className="shadow-md transition duration-300">
                            <Link to="/employeecard" className="text-white no-underline">
                                Create
                            </Link>
                        </Button>
                    </div>
                    {
                        loading ? (
                            <div className={"d-flex align-content-center"}>
                                <CircularProgress color="inherit" />
                            </div>
                        ) : error ? (
                            <h4 className={"text-center"}>{error}</h4>
                        ) : (
                            <List className="w-100 bg-white rounded-lg overflow-hidden shadow-lg">
                                {
                                    employees.map(employee =>
                                        <ListItem key={employee.id} className="d-flex align-items-center justify-content-between p-4 border-bottom border-primary hover:bg-muted transition duration-300">
                                            <ListItemAvatar>
                                                <Avatar alt="employee-avatar" src={employee.avatarUrl ?? ''} className="w-12 h-12 rounded mr-4 shadow-md" />
                                            </ListItemAvatar>
                                            <ListItemText
                                                primary={<Typography variant="h6" component="h2" className="font-weight-bold">{employee.lastName + " " + employee.firstName}</Typography>}
                                                secondary={<Typography variant="body2" className="text-muted">{employee.positionName}</Typography>}
                                            />
                                            <IconButton
                                                color="secondary"
                                                className="bg-accent text-accent px-4 py-2 rounded shadow-md hover:bg-accent/80 transition duration-300"
                                                onClick={() => navigate(`/employeeedit/${employee.id}`)}>
                                                <Edit />
                                            </IconButton>
                                        </ListItem>
                                    )
                                }
                            </List>
                        )
                    }
                </CardContent>
            </Card>
        </div>
    );
};

export default EmployeeList;

