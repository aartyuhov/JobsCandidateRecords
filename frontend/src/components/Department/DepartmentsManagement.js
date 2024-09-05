import React, { useEffect, useState } from 'react';
import ListDepartments from './ListDepartments';
import CreateDepartment from './CreateDepartment';
import { CircularProgress } from "@mui/material";
import api from "../../services/api";

const DepartmentsManagement = () => {
    const [departments, setDepartments] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await api.get(`/api/Department`);
                setDepartments(response.data);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
                setError('An error occurred while fetching the data. Please try again later.');
                setLoading(false);
            }
        };

        fetchData();
    }, [loading]);

    const handleCreate = (newDepartment) => {
        const fetchData = async (newDepartment) => {
            try {
                setLoading(true);
                await api.post(`/api/Department`, newDepartment);
            } catch (error) {
                console.error('Error creating department:', error);
            }
            setLoading(false);
        };
        fetchData(newDepartment);
        setDepartments([...departments, { id: departments.length + 1, ...newDepartment }]);
    };

    const handleDelete = (id) => {
        const fetchData = async (id) => {
            try {
                setLoading(true);
                await api.delete(`/api/Department/${id}`);
            } catch (error) {
                console.error('Error deleting department:', error);
            }
            setLoading(false);
        };
        fetchData(id);

        setDepartments(departments.filter((department) => department.id !== id));
    };

    const handleEdit = (updatedDepartment) => {
        const fetchData = async (newDepartment) => {
            try {
                setLoading(true);
                await api.put(`/api/Department/${newDepartment.id}`, newDepartment);
            } catch (error) {
                console.error('Error editing department:', error);
            }
            setLoading(false);
        };
        fetchData(updatedDepartment);

        const updatedDepartments = departments.map((department) =>
            department.id === updatedDepartment.id ? updatedDepartment : department
        );
        setDepartments(updatedDepartments);
    };

    return (
        <div>
            <h1>Departments Management</h1>
            <CreateDepartment onCreate={handleCreate} />
            {loading ? (
                <div className="d-flex align-content-center">
                    <CircularProgress color="inherit" />
                </div>
            ) : error ? (
                <h4 className="text-center">{error}</h4>
            ) : (
                <ListDepartments departments={departments} onDelete={handleDelete} onEdit={handleEdit} />
            )}
        </div>
    );
};

export default DepartmentsManagement;
