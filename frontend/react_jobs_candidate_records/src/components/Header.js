import React from 'react'
import './Header.css'
import logo from '../assets/images/logo.svg'

const Header = () => {
	return (
		<header className='header'>
			<div className='logo-container'>
				<img src={logo} alt='Company Logo' className='company-logo' />
			</div>
			<div className='button-67'>
				<button>Login</button>
				<button>Register</button>
			</div>
		</header>
	)
}

export default Header