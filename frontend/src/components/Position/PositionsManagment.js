import React, {useEffect, useState} from 'react';
import ListPositions from './ListPositions';
import CreatePosition from './CreatePosition';
import {CircularProgress} from "@mui/material";
import api from "../../services/api";
const PositionsManagement = () => {
    const [positions, setPositions] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await api.get(`/api/Position`);
                setPositions(response.data);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
                setError('An error occurred while fetching the data. Please try again later.');
                setLoading(false);
            }
        };

        fetchData();
    },[loading]);

    const handleCreate = (newPosition) => {
        const fetchData = async (newPosition) => {
            try {
                setLoading(true);
                await api.post(`/api/Position`, newPosition);
            } catch (error) {
                console.error('Error creating data:', error);
            }
            setLoading(false);
        };
        fetchData(newPosition);
        setPositions([...positions, { id: positions.length + 1, ...newPosition }]);
    };

    const handleDelete = (id) => {
        const fetchData = async (id) => {
            try {
                setLoading(true);
                await api.delete(`/api/Position/${id}`);
            } catch (error) {
                console.error('Error deleting data:', error);
            }
            setLoading(false);
        };
        fetchData(id);

        setPositions(positions.filter((position) => position.id !== id));
    };

    const handleEdit = (updatedPosition) => {
        const fetchData = async (newPosition) => {
            try {
                setLoading(true);
                await api.put(`/api/Position/${newPosition.id}`, newPosition);
            } catch (error) {
                console.error('Error editing data:', error);
            }
            setLoading(false);
        };
        fetchData(updatedPosition);

        const updatedPositions = positions.map((position) =>
            position.id === updatedPosition.id ? updatedPosition : position
        );
        setPositions(updatedPositions);
    };



    return (
        <div>
            <h1>Positions Management</h1>
            <CreatePosition onCreate={handleCreate} />
            { loading ? (
                <div className={"d-flex align-content-center"}>
                    <CircularProgress color="inherit" />
                </div>
            ) : error ? (
                <h4 className={"text-center"}>{error}</h4>
            ) : (
                <ListPositions positions={positions} onDelete={handleDelete} onEdit={handleEdit} />
            )}
        </div>
    );
};

export default PositionsManagement;
