import React, { useState } from 'react';
import {
    Paper,
    Typography,
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    TextField,
    IconButton,
    List,
    ListItem,
    ListItemText, Card
} from '@mui/material';
import { Edit } from '@mui/icons-material';
import 'bootstrap/dist/css/bootstrap.min.css';
import PositionList from "../small-components/PositionList";
import BasicDatePicker from "../small-components/BasicDatePicker";

const RequestsManager = () => {
    const [requests, setRequests] = useState([
        {
            id: 0,
            name: 'Test',
            publicationDate: '2024-08-03T12:31:19.209Z',
            numberEmployessRequired: 1,
            description: 'test',
            positionId: 1,
        },
    ]);

    const [showModal, setShowModal] = useState(false);
    const [currentRequest, setCurrentRequest] = useState(null);
    const [selectedPositionId, setSelectedPositionId] = useState('');

    const handleShowModal = (request = null) => {
        setCurrentRequest(request);
        setShowModal(true);
    };

    const handleCloseModal = () => {
        setCurrentRequest(null);
        setShowModal(false);
    };

    const handlePositionChange = (positionId) => {
        setSelectedPositionId(positionId);
    };

    const handleSave = (event) => {
        event.preventDefault();
        const form = event.currentTarget;
        const newRequest = {
            id: currentRequest ? currentRequest.id : requests.length,
            name: form.name.value,
            publicationDate: form.publicationDate.value,
            numberEmployessRequired: parseInt(form.numberEmployessRequired.value),
            description: form.description.value,
            positionId: parseInt(form.positionId.value),
        };

        if (currentRequest) {
            setRequests(requests.map(req => (req.id === currentRequest.id ? newRequest : req)));
        } else {
            setRequests([...requests, newRequest]);
        }

        handleCloseModal();
    };

    return (
        <Card sx={{ width: '85%', marginTop: 4, marginX: "auto" }}>
            <Paper className="p-3">
                <Typography variant="h4" className="mb-3">Job Requests (yours)</Typography>
                <Button variant="contained" color="primary" onClick={() => handleShowModal()}>New</Button>
                <List className="mt-3">
                    {requests.map((request) => (
                        <ListItem key={request.id} className="d-flex justify-content-between align-items-center">
                            <ListItemText
                                primary={request.name}
                                secondary={`Published on: ${new Date(request.publicationDate).toLocaleDateString()}`}
                            />
                            <div>
                                <IconButton color="primary" onClick={() => handleShowModal(request)}>
                                    <Edit />
                                </IconButton>
                            </div>
                        </ListItem>
                    ))}
                </List>
            </Paper>

            <Dialog open={showModal} onClose={handleCloseModal}>
                <DialogTitle>{currentRequest ? 'Edit Request' : 'Create New Request'}</DialogTitle>
                <DialogContent>
                    <form onSubmit={handleSave} id="request-form">
                        <TextField
                            className="mt-3"
                            margin="dense"
                            name="name"
                            label="Name"
                            type="text"
                            fullWidth
                            defaultValue={currentRequest ? currentRequest.name : ''}
                            required
                        />
                        <BasicDatePicker label="Publication date" name="publicationDate" defaultValue={currentRequest ? currentRequest.publicationDate : ''} value={currentRequest ? currentRequest.publicationDate : ''} className="w-100 mt-3" required />
                        <TextField
                            className="mt-3"
                            margin="dense"
                            name="numberEmployessRequired"
                            label="Number of Employees Required"
                            type="number"
                            fullWidth
                            defaultValue={currentRequest ? currentRequest.numberEmployessRequired : ''}
                            required
                        />
                        <TextField
                            className="mt-3"
                            margin="dense"
                            name="description"
                            label="Description"
                            type="text"
                            fullWidth
                            multiline
                            rows={3}
                            defaultValue={currentRequest ? currentRequest.description : ''}
                            required
                        />
                        <div className="mt-3">
                            <PositionList selectedPositionId={currentRequest ? currentRequest.positionId : ''} handlePositionChange={handlePositionChange} />
                        </div>
                    </form>
                </DialogContent>
                <DialogActions>
                    <Button form="request-form" type="submit" color="primary">
                        Save
                    </Button>
                    <Button onClick={handleCloseModal} color="secondary">
                        Cancel
                    </Button>
                </DialogActions>
            </Dialog>
        </Card>
    );
};

export default RequestsManager;
