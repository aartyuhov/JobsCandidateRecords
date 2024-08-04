import React, {useEffect, useState} from 'react';
import ListPositions from './ListPositions';
import CreatePosition from './CreatePosition';
import axios from "axios";
import {CircularProgress} from "@mui/material";
const PositionsManagement = () => {
    const [positions, setPositions] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get(`/api/Position`);
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
                await axios.post(`/api/Position`, newPosition);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
            setLoading(false);
        };
        fetchData(newPosition);
        setPositions([...positions, { id: positions.length + 1, ...newPosition }]);
    };

    const handleDelete = (id) => {
        setPositions(positions.filter((position) => position.id !== id));
    };

    const handleEdit = (updatedPosition) => {
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
