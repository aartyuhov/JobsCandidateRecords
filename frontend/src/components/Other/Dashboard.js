import React from 'react'
import './Dashboard.css'
import vacationImage1 from '../../assets/images/vacation1.jpg'
import vacationImage2 from '../../assets/images/vacation2.jpg'
import vacationImage3 from '../../assets/images/vacation3.jpg'
import vacationImage4 from '../../assets/images/vacation4.jpg'

const EmployeeCard = ({ avatarSrc, employeeName }) => (
	<li className='employee-card'>
		<div className='employee-info'>
			<img src={avatarSrc} alt='employee-avatar' className='employee-avatar' />
			<span className='employee-name'>{employeeName}</span>
		</div>
		<button className='button-view-profile'>View Profile</button>
	</li>
)
const LeaveRequestCard = ({
	title,
	content,
	backgroundImage,
	backgroundPosition,
}) => (
	<div
		className='leave-request-card'
		style={{
			backgroundImage: `url(${backgroundImage})`,
			backgroundPosition: backgroundPosition,
		}}
	>
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
							backgroundImage={vacationImage1}
							backgroundPosition='left top'
						/>
					</div>
					<div className='col'>
						<LeaveRequestCard
							title='Sick Leave'
							content='1 pending request'
							backgroundImage={vacationImage2}
							backgroundPosition='right center'
						/>
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
							backgroundImage={vacationImage3}
							backgroundPosition='right bottom'
						/>
					</div>
					<div className='col'>
						<LeaveRequestCard
							title='Improvement Areas'
							content='Communication Skills'
							backgroundImage={vacationImage4}
							backgroundPosition='right bottom'
						/>
					</div>
				</div>
			</div>
		</div>
	)
}

export default Dashboard