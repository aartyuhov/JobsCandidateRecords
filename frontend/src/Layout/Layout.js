import React, { useState } from 'react'
import { Link } from 'react-router-dom'
import './Layout.css'
import { Avatar } from '@mui/material'
import DashboardIcon from '../SVGIcons/DashboardIcon'
import MainIcon from '../SVGIcons/MainIcon'
import LogoutIcon1 from '../SVGIcons/LogoutIcon'
import LogoutIcon2 from '../SVGIcons/LogoutIconAfter'
import EmployeeListIcon from '../SVGIcons/EmployeeListIcon'
import RequestsManagerIcon from '../SVGIcons/RequestsManagerIcon'
import { FormatListBulleted } from '@mui/icons-material'

const Layout = ({
	username = '',
	children,
	logoutHandler = () => console.log('logout'),
}) => {
	const [isSidebarVisible, setSidebarVisible] = useState(false)
	const [isHoveringLogout, setIsHoveringLogout] = useState(false)

	const showSidebar = () => {
		setSidebarVisible(true)
	}

	const hideSidebar = () => {
		setSidebarVisible(false)
	}

	const handleMouseEnter = () => {
		setIsHoveringLogout(true)
	}

	const handleMouseLeave = () => {
		setIsHoveringLogout(false)
	}

	return (
		<div className='d-flex min-vh-100'>
			<aside
				className={`sidebar ${isSidebarVisible ? 'visible' : ''}`}
				onMouseEnter={showSidebar}
				onMouseLeave={hideSidebar}
			>
				<nav>
					<div className='d-flex justify-content-between align-items-center p-4'>
						<div className='text-lg fw-bold'>
							<MainIcon />
							<span className='px-3'>HR app</span>
						</div>
					</div>
					<ul className='list-unstyled'>
						<li className='mb-1 d-flex align-items-center cursor-pointer'>
							<Link to='/' className='nav-item text-white no-underline'>
								<DashboardIcon/> <span className='px-3'>Dashboard</span>
							</Link>
						</li>
						<li className='mb-1 d-flex align-items-center cursor-pointer'>
							<Link
								to='/myrequests'
								className='nav-item text-white no-underline'
							>
								<RequestsManagerIcon/>{' '}
								<span className='px-3'>Your Requests</span>
							</Link>
						</li>
						<hr/>
						<h5 className='mb-1 mx-2'>HR</h5>
						<li className='mb-1 d-flex align-items-center cursor-pointer'>
							<Link
								to='/employees'
								className='nav-item text-white no-underline'
							>
								<EmployeeListIcon/> <span className='px-3'>Employee list</span>
							</Link>
						</li>
						<li className='mb-1 d-flex align-items-center cursor-pointer'>
							<Link
								to='/candidates'
								className='nav-item text-white no-underline'
							>
								<EmployeeListIcon/>{' '}
								<span className='px-3'>Candidate list</span>
							</Link>
						</li>
						<hr/>
						<h5 className='mb-1 mx-2'>Head of Department</h5>
						<li className='mb-1 d-flex align-items-center cursor-pointer'>
							<Link
								to='/allrequests'
								className='nav-item text-white no-underline'
							>
								<RequestsManagerIcon/>{' '}
								<span className='px-3'>Requests</span>
							</Link>
						</li>
						<hr/>
						<h5 className='mb-1 mx-2'>Admin</h5>
						<li className='mb-1 d-flex align-items-center cursor-pointer'>
							<Link
								to='/positions'
								className='nav-item text-white no-underline'
							>
								<FormatListBulleted className='text-dark'/>{' '}
								<span className='px-3'>Positions</span>
							</Link>
						</li>
						<li className='mb-1 d-flex align-items-center cursor-pointer'>
							<Link
								to='/departments'
								className='nav-item text-white no-underline'
							>
								<FormatListBulleted className='text-dark'/>{' '}
								<span className='px-3'>Departments</span>
							</Link>
						</li>
						<li className='mb-1 d-flex align-items-center cursor-pointer'>
							<Link
								to='/companies'
								className='nav-item text-white no-underline'
							>
								<FormatListBulleted className='text-dark'/>{' '}
								<span className='px-3'>Companies</span>
							</Link>
						</li>
					</ul>
				</nav>
			</aside>
			<div className={`content ${isSidebarVisible ? 'shifted' : ''}`}>
				<div className='d-flex justify-content-between align-items-center border-bottom pb-3 mb-3'>
					<div className='d-flex align-items-center ms-auto'>
						<Link
							to='/account'
							className='nav-item fw-bold no-underline cursor-pointer'
						>
							{username ? <span className='me-3'>{username}</span> : ''}
						</Link>
						<Avatar alt='User Avatar'>
							{username && username[0] ? username[0].toUpperCase() : ''}
						</Avatar>
						<i
							className='nav-item cursor-pointer'
							onClick={logoutHandler}
							onMouseEnter={handleMouseEnter}
							onMouseLeave={handleMouseLeave}
						>
							{isHoveringLogout ? <LogoutIcon2 /> : <LogoutIcon1 />}
						</i>
					</div>
				</div>
				<div className='flex-grow-1'>{children}</div>
			</div>
		</div>
	)
}

export default Layout