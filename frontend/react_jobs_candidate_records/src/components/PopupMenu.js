import React, { useState } from 'react'
import './PopupMenu.css'
import logo from '../assets/images/logo.svg'

const PopupMenu = () => {
	const [isVisible, setIsVisible] = useState(false)

	const menuItems = [
		{ name: 'Home', icon: '🏠' },
		{ name: 'Profile', icon: '👨🏻‍💼' },
		{ name: 'Contact', icon: '📞' },
		{ name: 'Summary', icon: '📝' },
		{ name: 'About', icon: 'ℹ️' },
		{ name: 'Settings', icon: '⚙️' },
	]

	return (
		<div
			className={`popup-menu ${isVisible ? 'visible' : ''}`}
			onMouseEnter={() => setIsVisible(true)}
			onMouseLeave={() => setIsVisible(false)}
		>
			<div className='menu-grid'>
				{menuItems.map((item, index) => (
					<div key={index} className='menu-item'>
						<span className='menu-icon'>{item.icon}</span>
						<span className='menu-text'>{item.name}</span>
					</div>
				))}
			</div>
		</div>
	)
}

export default PopupMenu