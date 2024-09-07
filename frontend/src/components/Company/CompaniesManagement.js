import React, { useEffect, useState } from 'react';
import ListCompanies from './ListCompanies';
import CreateCompany from './CreateCompany';
import { CircularProgress } from "@mui/material";
import api from "../../services/api";

const CompaniesManagement = () => {
    const [companies, setCompanies] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await api.get(`/api/Companies`);
                setCompanies(response.data);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
                setError('An error occurred while fetching the data. Please try again later.');
                setLoading(false);
            }
        };

        fetchData();
    }, [loading]);

    const handleCreate = (newCompany) => {
        const fetchData = async (newCompany) => {
            try {
                setLoading(true);
                await api.post(`/api/Companies`, newCompany);
            } catch (error) {
                console.error('Error creating company:', error);
            }
            setLoading(false);
        };
        fetchData(newCompany);
        setCompanies([...companies, { id: companies.length + 1, ...newCompany }]);
    };

    const handleDelete = (id) => {
        const fetchData = async (id) => {
            try {
                setLoading(true);
                await api.delete(`/api/Companies/${id}`);
            } catch (error) {
                console.error('Error deleting company:', error);
            }
            setLoading(false);
        };
        fetchData(id);

        setCompanies(companies.filter((company) => company.id !== id));
    };

    const handleEdit = (updatedCompany) => {
        const fetchData = async (newCompany) => {
            try {
                setLoading(true);
                await api.put(`/api/Companies/${newCompany.id}`, newCompany);
            } catch (error) {
                console.error('Error editing company:', error);
            }
            setLoading(false);
        };
        fetchData(updatedCompany);

        const updatedCompanies = companies.map((company) =>
            company.id === updatedCompany.id ? updatedCompany : company
        );
        setCompanies(updatedCompanies);
    };

    return (
        <div>
            <h1>Companies Management</h1>
            <CreateCompany onCreate={handleCreate} />
            {loading ? (
                <div className="d-flex align-content-center">
                    <CircularProgress color="inherit" />
                </div>
            ) : error ? (
                <h4 className="text-center">{error}</h4>
            ) : (
                <ListCompanies companies={companies} onDelete={handleDelete} onEdit={handleEdit} />
            )}
        </div>
    );
};

export default CompaniesManagement;
