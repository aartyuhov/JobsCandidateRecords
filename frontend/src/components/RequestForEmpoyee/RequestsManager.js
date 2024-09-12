import React, { useState, useEffect } from 'react';
import { Edit, Delete, Visibility } from '@mui/icons-material';
import Chip from '@mui/material/Chip';
import PositionList from '../small-components/PositionList';
import BasicDatePicker from '../small-components/BasicDatePicker';
import ProgressiveImage from '../small-components/ProgressiveImage';
import api from '../../services/api';
import './RequestsManager.css';
import RequestsFormImage from '../../assets/images/RequestsForm.jpg';
import { RequestForEmployeeStatus } from "../../constants/requeststatus";

const RequestsManager = ({ viewType }) => {
	const [requests, setRequests] = useState([]);
	const [showModal, setShowModal] = useState(false);
	const [currentRequest, setCurrentRequest] = useState(null);
	const [selectedPositionId, setSelectedPositionId] = useState('');
	const [currentUser, setCurrentUser] = useState(null);
	const [isMounted, setIsMounted] = useState(false);
	const [viewOnly, setViewOnly] = useState(false);

	useEffect(() => {
		fetchRequests().catch(error => console.log(error));
		setIsMounted(true);
	}, [currentUser, viewType]);

	useEffect(() => {
		if (currentRequest) {
			setSelectedPositionId(currentRequest.positionId || '');
		} else {
			setSelectedPositionId('');
		}
	}, [currentRequest]);

	useEffect(() => {
		if (showModal) {
			document.body.style.overflow = 'hidden';
		} else {
			document.body.style.overflow = 'unset';
		}

		return () => {
			document.body.style.overflow = 'unset';
		};
	}, [showModal]);

	const fetchRequests = async () => {
		if(!currentUser)
		{
			await fetchCurrentUser();
		}
		else
		{
			const url = viewType === 'my' ? `/api/RequestForEmployeeDTO/getByEmployeeCreatedId?requestedEmployeeId=${currentUser.id}` : '/api/RequestForEmployeeDTO/noClosed';
			try {
				const response = await api.get(url);
				setRequests(response.data);
			} catch (error) {
				console.error('Error fetching requests:', error);
			}
		}
	};

	const fetchCurrentUser = async () => {
		try {
			const response = await api.get('/api/Users/get-tuple');
			setCurrentUser(response.data.employeeDto);
		} catch (error) {
			console.error('Error fetching current user data:', error);
		}
	};

	const handleShowModal = (request = null, viewOnly = false) => {
		setCurrentRequest(request);
		setViewOnly(viewOnly);
		setShowModal(true);

		window.scrollTo({
			top: 0,
		});
	};

	const handleCloseModal = () => {
		setCurrentRequest(null);
		setSelectedPositionId('');
		setShowModal(false);
	};

	const handlePositionChange = (positionId) => {
		setSelectedPositionId(positionId);
	};

	const handleSave = async (event) => {
		event.preventDefault();
		const form = event.currentTarget;
		const newRequest = {
			id: currentRequest ? currentRequest.id : 0,
			name: form.name.value.trim(),
			publicationDate: form.publicationDate.value
				? new Date(form.publicationDate.value).toISOString()
				: null,
			numberEmployessRequired:
				parseInt(form.numberEmployessRequired.value) || 0,
			description: form.description.value.trim(),
			positionId: selectedPositionId ? parseInt(selectedPositionId) : null,
			requestedEmployeeId: currentUser ? currentUser.id : null,
		};

		try {
			if (currentRequest) {
				await api.put('/api/RequestForEmployeeDTO', newRequest);
			} else {
				await api.post('/api/RequestForEmployeeDTO', newRequest);
			}
			fetchRequests();
			handleCloseModal();
		} catch (error) {
			console.error('Error saving request:', error);
			if (error.response) {
				console.error('Response data:', error.response.data);
				if (error.response.data.errors) {
					console.error('Validation errors:', error.response.data.errors);
				}
			}
		}
	};

	const handleDelete = async (requestId) => {
		try {
			await api.delete(`/api/RequestForEmployee/${requestId}`);
			setRequests(requests.filter((request) => request.id !== requestId));
		} catch (error) {
			console.error('Error deleting request:', error);
		}
	};

	const getStatusLabel = (statusValue) => {
		const status = Object.values(RequestForEmployeeStatus).find(
			(s) => s.value === statusValue
		);
		return status ? status.label : 'Unknown';
	};

	return (
		<div className='requests-manager-container'>
			<div className='requests-card'>
				<h1 className='requests-title'>{viewType === 'my' ? 'Your Requests' : 'Job Requests'}</h1>
				{viewType !== 'my' && (
					<button className='new-requests-btn' onClick={() => handleShowModal()}>
						Add Request
					</button>
				)}
				<ul className={`requests-list ${isMounted ? 'mounted' : ''}`}>
					{requests.map((request, index) => (
						<li
							key={request.id}
							className={`request-list-item ${
								index % 2 === 0 ? 'even' : 'odd'
							}`}
							style={{ animationDelay: `${index * 0.1}s` }}
						>
							<div className='request-details'>
								<strong>{request.name}</strong>
								<strong> </strong>
								<small>
									(From {new Date(request.publicationDate).toLocaleDateString()})
								</small>
								<Chip
									label={request.positionName}
									variant="outlined"
									sx={{
										marginLeft: '10px',
										color: 'white',
										borderColor: '#800000',
									}}
								/>
								<Chip
									label={getStatusLabel(request.requestStatus)}
									variant="outlined"
									color="primary"
									className="request-status-chip"
									style={{ marginLeft: '10px' }} // Добавляем отступ слева
								/>
							</div>
							<div className='request-actions'>
								<button
									className='view-btn'
									onClick={() => handleShowModal(request, true)}
								>
									<Visibility />
								</button>
								{currentUser &&
									request.requestedEmployeeId === currentUser.id && (
										<>
											<button
												className='edit-btn'
												onClick={() => handleShowModal(request)}
											>
												<Edit />
											</button>
											<button
												className='delete-btn'
												onClick={() => handleDelete(request.id)}
											>
												<Delete />
											</button>
										</>
									)}
							</div>
						</li>
					))}
				</ul>
			</div>

			{showModal && (
				<div className={`modal-overlay ${showModal ? 'active' : ''}`}>
					<div className='modal-content'>
						<h2>
							{viewOnly
								? 'View Request'
								: currentRequest
									? 'Edit Request'
									: 'Create New Request'}
						</h2>
						<form onSubmit={handleSave} id='request-form'>
							<div className='input-wrapper'>
								<input
									id='name'
									className='input-field'
									name='name'
									type='text'
									defaultValue={currentRequest ? currentRequest.name : ''}
									required
									placeholder=' '
									disabled={viewOnly}
								/>
								<label htmlFor='name'>Name</label>
							</div>

							<div className='date-picker-wrapper'>
								<BasicDatePicker
									label='Publication date'
									name='publicationDate'
									defaultValue={
										currentRequest ? currentRequest.publicationDate : ''
									}
									value={currentRequest ? currentRequest.publicationDate : ''}
									required
									disabled={viewOnly}
								/>
							</div>

							<div className='input-wrapper'>
								<input
									id='numberEmployessRequired'
									className='input-field'
									name='numberEmployessRequired'
									type='number'
									defaultValue={
										currentRequest ? currentRequest.numberEmployessRequired : ''
									}
									required
									placeholder=' '
									disabled={viewOnly}
								/>
								<label htmlFor='numberEmployessRequired'>
									Number of Employees Required
								</label>
							</div>

							<div className='input-wrapper'>
                                <textarea
									id='description'
									className='input-field'
									name='description'
									defaultValue={
										currentRequest ? currentRequest.description : ''
									}
									required
									placeholder=' '
									disabled={viewOnly}
								></textarea>
								<label htmlFor='description'>Description</label>
							</div>

							<div style={{ marginright: '50px', marginBottom: '20px' }}>
								<PositionList
									selectedPositionId={selectedPositionId}
									handlePositionChange={handlePositionChange}
									disabled={viewOnly}
								/>
							</div>

							<div className='modal-actions'>
								{(!viewOnly) && (
									<button type='submit' className='save-btn'>
										Save
									</button>
								)}
								<button onClick={handleCloseModal} className='cancel-btn'>
									Cancel
								</button>
							</div>
						</form>
					</div>
					<div className='modal-image'>
						<ProgressiveImage
							src={RequestsFormImage}
							alt='Requests Form'
							className='request-form-image'
						/>
					</div>
				</div>
			)}
		</div>
	);
};

export default RequestsManager;
