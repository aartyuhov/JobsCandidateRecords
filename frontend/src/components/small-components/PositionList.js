import {
    CircularProgress,
    FormControl,
    InputLabel,
    MenuItem,
    Select
} from '@mui/material';
import React, {useEffect, useState} from 'react';
import api from "../../services/api";

const PositionList = ({selectedPositionId, handlePositionChange}) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await api.get(`/api/Position`);
                if (response.status !== 200) {
                    throw new Error(`HTTP error! Status: ${response.status}`);
                }
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
                <div className={"d-flex align-content-center"}>
                    <CircularProgress color="inherit" />
                </div>
            ) : error ? (
                <h4 className={"text-center"}>{error}</h4>
            ) : (
                <FormControl variant="outlined" fullWidth required>
                    <InputLabel>Position</InputLabel>
                    <Select
                        label="Position"
                        name="positionId"
                        defaultValue={selectedPositionId}
                        onChange={(e) => handlePositionChange(e.target.value)}
                    >
                        {data.map(item =>
                            <MenuItem key={item.id} value={item.id}>{item.title}</MenuItem>
                        )}
                    </Select>
                </FormControl>
            )}
        </>
    );
};

export default PositionList;
