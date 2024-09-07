import React, {useEffect, useState} from 'react';
import {Button, CircularProgress, FormControl, InputLabel, MenuItem, Select, TextField} from "@mui/material";
import api from "../../services/api";

const CreatePosition = ({ onCreate }) => {
    const [newPosition, setNewPosition] = useState({ title: '', responsibilities: '', departmentId: 0 });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setNewPosition((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    const handleCreate = (e) => {
        e.preventDefault();
        onCreate(newPosition);
        setNewPosition({ id:'0', title: '', responsibilities: '', departmentId: newPosition.departmentId });
    };

    return (
        <form autoComplete="off" className="d-flex align-content-start gap-4 p-3" onSubmit={handleCreate}>
            <TextField
                label="Title"
                name="title"
                value={newPosition.title}
                onChange={handleChange}
                fullWidth
                margin="normal"
                required
            />
            <TextField
                label="Responsibilities"
                name="responsibilities"
                value={newPosition.responsibilities}
                onChange={handleChange}
                fullWidth
                margin="normal"
            />
            <DepartmentList onChange={handleChange}/>
            <Button variant="contained" color="primary" className="w-25 my-auto" type="submit">
                Create
            </Button>
        </form>
    );
};

const DepartmentList = ({onChange, departmentId}) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await api.get(`/api/Department`);
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
                    <FormControl variant="outlined" fullWidth required margin="normal">
                        <InputLabel>Department</InputLabel>
                        <Select label="Department" name="departmentId" defaultValue={departmentId} onChange={onChange}>
                            {data.map(item =>
                                <MenuItem key={item.id} value={item.id}>{item.name}</MenuItem>
                            )}
                        </Select>
                    </FormControl>
                )
            }
        </>
    );
};

export default CreatePosition;
