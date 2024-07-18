import React from 'react';
import { Button, Select, MenuItem, FormControl, Card, CardContent, Typography, Avatar, List, ListItem, ListItemAvatar, ListItemText, IconButton } from '@mui/material';
import 'bootstrap/dist/css/bootstrap.min.css';
import {Edit} from "@mui/icons-material";

const EmployeeList = () => {
    return (
        <div className="d-flex align-content-start justify-content-center min-vh-100 bg-light text-dark">
            <Card className="w-100 max-w-3xl shadow-lg rounded-lg">
                <CardContent>
                    <div className="d-flex justify-content-between align-items-center mb-4">
                        <Typography variant="h4" component="h1" className="fw-bold">Employee List</Typography>
                        <Button variant="contained" color="primary" className="shadow-md transition duration-300">Create</Button>
                    </div>
                    <div className="d-flex align-items-center mb-4">
                        <FormControl className="mr-4">
                            <Select id="filter" defaultValue="all" className="border-primary rounded-md shadow-md focus:outline-none focus:ring-2 focus:ring-primary transition duration-300">
                                <MenuItem value="all">All</MenuItem>
                                <MenuItem value="active">Active</MenuItem>
                                <MenuItem value="inactive">Inactive</MenuItem>
                            </Select>
                        </FormControl>
                    </div>
                    <List className="w-100 bg-white rounded-lg overflow-hidden shadow-lg">
                        <ListItem className="d-flex align-items-center justify-content-between p-4 border-bottom border-primary hover:bg-muted transition duration-300">
                            <ListItemAvatar>
                                <Avatar alt="employee-avatar" src="https://placehold.co/50?text=👩" className="w-12 h-12 rounded mr-4 shadow-md" />
                            </ListItemAvatar>
                            <ListItemText
                                primary={<Typography variant="h6" component="h2" className="font-weight-bold">Employee Name</Typography>}
                                secondary={<Typography variant="body2" className="text-muted">Position</Typography>}
                            />
                            <IconButton color="secondary" className="bg-accent text-accent px-4 py-2 rounded shadow-md hover:bg-accent/80 transition duration-300">
                                <Edit />
                            </IconButton>
                        </ListItem>
                    </List>
                </CardContent>
            </Card>
        </div>
    );
};

export default EmployeeList;
