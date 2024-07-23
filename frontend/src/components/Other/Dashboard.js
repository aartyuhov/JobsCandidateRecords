import React from 'react'
import './Dashboard.css'

const EmployeeCard = ({ avatarSrc, employeeName }) => (
	<li className='employee-card'>
		<div className='employee-info'>
			<img src={avatarSrc} alt='employee-avatar' className='employee-avatar' />
			<span className='employee-name'>{employeeName}</span>
		</div>
		<button className='button-view-profile'>View Profile</button>
	</li>
)

const LeaveRequestCard = ({ title, content }) => (
	<div className='leave-request-card'>
		<h3 className='leave-request-title'>{title}</h3>
		<p className='leave-request-content'>{content}</p>
	</div>
)

const Dashboard = () => {
	return (
		<div className='dashboard'>
			<h1 className='dashboard-title'>HR Dashboard</h1>

			<div className='card employee-list'>
				<h2 className='card-title'>Employee List</h2>
				<ul className='employee-list-items'>
					<EmployeeCard
						avatarSrc='https://placehold.co/40x40?text=👩'
						employeeName='Jane Doe'
					/>
					<EmployeeCard
						avatarSrc='https://placehold.co/40x40?text=👨'
						employeeName='John Smith'
					/>
				</ul>
			</div>

			<div className='card leave-requests mt-4'>
				<h2 className='card-title'>Leave Requests</h2>
				<div className='row'>
					<div className='col'>
						<LeaveRequestCard
							title='Vacation Leave'
							content='No pending requests'
						/>
					</div>
					<div className='col'>
						<LeaveRequestCard title='Sick Leave' content='1 pending request' />
					</div>
				</div>
			</div>

			<div className='card performance-overview mt-4'>
				<h2 className='card-title'>Performance Overview</h2>
				<div className='row'>
					<div className='col'>
						<LeaveRequestCard
							title='Top Performers'
							content='John Smith, Jane Doe'
						/>
					</div>
					<div className='col'>
						<LeaveRequestCard
							title='Improvement Areas'
							content='Communication Skills'
						/>
					</div>
				</div>
			</div>
		</div>
	)
}

export default Dashboard