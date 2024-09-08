import React, { useEffect, useState } from 'react';
import { Button, CircularProgress, FormControl, InputLabel, MenuItem, Select, TextField } from "@mui/material";
import api from "../../services/api";

const CreateDepartment = ({ onCreate }) => {
    const [newDepartment, setNewDepartment] = useState({ name: '', description: '', companyId: 0 });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setNewDepartment((prev) => ({
            ...prev,
            [name]: value,
        }));
    };

    const handleCreate = (e) => {
        e.preventDefault();
        onCreate(newDepartment);
        setNewDepartment({ id: '0', name: '', description: '', companyId: newDepartment.companyId });
    };

    return (
        <form autoComplete="off" className="d-flex align-content-start gap-4 p-3" onSubmit={handleCreate}>
            <TextField
                label="Name"
                name="name"
                value={newDepartment.name}
                onChange={handleChange}
                fullWidth
                margin="normal"
                required
            />
            <TextField
                label="Description"
                name="description"
                value={newDepartment.description}
                onChange={handleChange}
                fullWidth
                margin="normal"
            />
            <CompanyList onChange={handleChange} />
            <Button variant="contained" color="primary" className="w-25 my-auto" type="submit">
                Create
            </Button>
        </form>
    );
};

const CompanyList = ({ onChange, companyId }) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await api.get(`/api/Companies`);
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
            {loading ? (
                <div className="d-flex align-content-center">
                    <CircularProgress color="inherit" />
                </div>
            ) : error ? (
                <h4 className="text-center">{error}</h4>
            ) : (
                <FormControl variant="outlined" fullWidth required margin="normal">
                    <InputLabel>Company</InputLabel>
                    <Select label="Company" name="companyId" defaultValue={companyId} onChange={onChange}>
                        {data.map(item => (
                            <MenuItem key={item.id} value={item.id}>{item.name}</MenuItem>
                        ))}
                    </Select>
                </FormControl>
            )}
        </>
    );
};

export default CreateDepartment;
