import React from 'react'
import Header from './components/Header'
import MainContent from './components/MainContent'
import PopupMenu from './components/PopupMenu'
import './App.css'

function App() {
	return (
		<div className='App'>
			<Header />
			<PopupMenu />
			<div className='content-wrapper'>
				<MainContent />
			</div>
		</div>
	)
}

export default App