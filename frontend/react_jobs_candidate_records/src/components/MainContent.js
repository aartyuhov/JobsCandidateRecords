import React, { useEffect, useRef, useState } from 'react'
import './MainContent.css'
import sampleImage1 from '../assets/images/sample_image_1.jpg'
import sampleImage2 from '../assets/images/sample_image_2.jpg'
import sampleImage3 from '../assets/images/sample_image_3.jpg'
import What_is_Figma from '../assets/images/What_is_Figma.jpg'
import team_collaboration from '../assets/images/team_collaboration.jpg'
import Design_Award from '../assets/images/Design_Award.png'
import working_from_home from '../assets/images/working_from_home.jpg'
import wellness_program from '../assets/images/wellness_program.jpg'
import modern_office_space from '../assets/images/modern_office_space.jpg'

const MainContent = () => {
	const mainContentRef = useRef(null)
	const scrollAnimationRef = useRef(null)
	const velocityRef = useRef(0)
	const lastTimeRef = useRef(Date.now())
	const [panelOpacity, setPanelOpacity] = useState(1)

	useEffect(() => {
		const mainContent = mainContentRef.current

		const smoothScroll = () => {
			const now = Date.now()
			const dt = (now - lastTimeRef.current) / 1000
			lastTimeRef.current = now

			const maxScroll = mainContent.scrollWidth - mainContent.clientWidth

			velocityRef.current *= Math.pow(0.95, dt * 30)

			mainContent.scrollLeft += velocityRef.current * dt

			if (mainContent.scrollLeft <= 1) {
				velocityRef.current = 1
				mainContent.scrollLeft = 1
			} else if (mainContent.scrollLeft >= maxScroll) {
				velocityRef.current = 1
				mainContent.scrollLeft = maxScroll
			}

			const parallaxFactor = 0.5
			document.body.style.backgroundPositionX = `-${
				mainContent.scrollLeft * parallaxFactor
			}px`

			const fadeStartPoint = 300
			const fadeEndPoint = 500
			const newOpacity = Math.max(
				0,
				1 -
					(mainContent.scrollLeft - fadeStartPoint) /
						(fadeEndPoint - fadeStartPoint)
			)
			setPanelOpacity(newOpacity)

			handleScroll()

			if (Math.abs(velocityRef.current) > 0.1 || scrollAnimationRef.current) {
				scrollAnimationRef.current = requestAnimationFrame(smoothScroll)
			} else {
				scrollAnimationRef.current = null
			}
		}

		const handleScroll = () => {
			const scrollPosition = mainContent.scrollLeft
			const middlePanel = document.querySelector('.middle-panel')
			const fadeInStart = 1
			const fadeOutEnd = 4100
			const stopScrollPoint = 3080

			if (scrollPosition > fadeInStart && scrollPosition < fadeOutEnd) {
				middlePanel.classList.add('visible')
			} else {
				middlePanel.classList.remove('visible')
			}

			if (scrollPosition > stopScrollPoint) {
				middlePanel.style.transform = `translateX(calc(-50% + ${
					stopScrollPoint - scrollPosition
				}px))`
			} else {
				middlePanel.style.transform = `translateX(-50%)`
			}
		}

		const handleWheel = event => {
			event.preventDefault()

			velocityRef.current += event.deltaY * 2

			if (!scrollAnimationRef.current) {
				scrollAnimationRef.current = requestAnimationFrame(smoothScroll)
			}
		}

		mainContent.addEventListener('wheel', handleWheel, { passive: false })
		mainContent.addEventListener('scroll', handleScroll)

		return () => {
			mainContent.removeEventListener('wheel', handleWheel)
			mainContent.removeEventListener('scroll', handleScroll)
			cancelAnimationFrame(scrollAnimationRef.current)
		}
	}, [])

	return (
		<main className='main-content' ref={mainContentRef}>
			<div
				className='main-content-panel start-panel'
				style={{ opacity: panelOpacity }}
			>
				<div className='greeting-container'>
					<h3 className='greeting-text'>Welcome to</h3>
					<div className='campaign-name'>
						<span className='letter'>F</span>
						<span className='letter'>I</span>
						<span className='letter'>G</span>
						<span className='letter'>M</span>
						<span className='letter'>A</span>
					</div>
					<p className='campaign-slogan'>Design, Collaborate, Create</p>
				</div>
			</div>
			<div className='main-content-item'>
				<h2>What is Figma?</h2>
				<div className='img-wrapper img-wrapper-right'>
					<div style={{ backgroundImage: `url(${What_is_Figma})` }}></div>
				</div>
				<p>
					Figma is a cloud-based real-time collaboration tool used by IT
					companies in 2023 for UX design, presentations, brainstorming,
					business roadmaps, illustrations, and emoji creation. As a product
					designer, it manages curriculums, runs live online workshops, and even
					writes books, making it akin to Google Docs.
				</p>
			</div>
			<div className='main-content-item'>
				<h2>Figma Unveils New Collaboration Features</h2>
				<div className='img-wrapper img-wrapper-center'>
					<div style={{ backgroundImage: `url(${team_collaboration})` }}></div>
				</div>
				<p>
					Figma has announced a series of new features aimed at enhancing team
					collaboration. These updates include real-time comments, improved
					version control, and integrated project management tools. The goal is
					to make it easier for design teams to work together seamlessly,
					regardless of their location. "We believe that great design comes from
					great collaboration," said Figma's CEO. "These new features are
					designed to bring teams closer and streamline the design process."
				</p>
			</div>
			<div className='main-content-item'>
				<h2>Figma Receives Prestigious Design Award</h2>
				<div className='img-wrapper img-wrapper-right'>
					<div style={{ backgroundImage: `url(${Design_Award})` }}></div>
				</div>
				<p>
					Figma has been honored with the prestigious Innovation in Design
					Award, recognizing its groundbreaking contributions to the design
					industry. The award highlights Figma's commitment to providing
					designers with cutting-edge tools that foster creativity and
					collaboration. The judges praised Figma for its user-friendly
					interface and robust feature set, which have made it a favorite among
					designers worldwide.
				</p>
			</div>
			<div className='spacer'></div>
			<div className='main-content-panel middle-panel'>
				<h3>Announcements</h3>
				<p>Discover what we can do for you</p>
			</div>
			<div className='spacer'></div>
			<div className='main-content-item'>
				<h2>We're Hiring! Join Our Dynamic Team</h2>
				<div className='img-wrapper img-wrapper-center'>
					<div style={{ backgroundImage: `url(${sampleImage2})` }}></div>
				</div>
				<p>
					Figma is excited to announce that we are expanding our team! We are
					looking for passionate and talented individuals to join us in various
					roles, including software development, design, marketing, and customer
					support. At Figma, we value creativity, collaboration, and innovation.
					If you're looking for a dynamic work environment where you can grow
					and make a difference, we want to hear from you. Check out our careers
					page for more details on open positions.
				</p>
			</div>
			<div className='main-content-item'>
				<h2>New Flexible Working Hours Policy</h2>
				<div className='img-wrapper img-wrapper-center'>
					<div style={{ backgroundImage: `url(${working_from_home})` }}></div>
				</div>
				<p>
					In our ongoing effort to provide a supportive and inclusive workplace,
					Figma is introducing a new flexible working hours policy. Employees
					can now choose their working hours to better balance their
					professional and personal lives. This policy is designed to
					accommodate different lifestyles and time zones, making it easier for
					our team members to work when they are most productive. We believe
					that flexibility is key to fostering a healthy and happy workforce.
				</p>
			</div>
			<div className='main-content-item'>
				<h2>Figma Rolls Out Enhanced Benefits Package</h2>
				<div className='img-wrapper img-wrapper-left'>
					<div style={{ backgroundImage: `url(${wellness_program})` }}></div>
				</div>
				<p>
					Figma is committed to the well-being of our employees. We are pleased
					to announce an enhanced benefits package that includes comprehensive
					health insurance, increased paid time off, and access to wellness
					programs. Additionally, we are introducing new parental leave policies
					to support our team members during important life events. We believe
					that taking care of our employees is essential to building a strong
					and motivated team.
				</p>
			</div>
			<div className='main-content-item'>
				<h2>Figma Opens New State-of-the-Art Offices</h2>
				<div className='img-wrapper img-wrapper-right'>
					<div
						style={{ backgroundImage: `url(${modern_office_space})` }}
					></div>
				</div>
				<p>
					As part of our commitment to providing a great working environment,
					Figma is opening new state-of-the-art office spaces in several key
					locations. These offices are designed with collaboration and comfort
					in mind, featuring open floor plans, ergonomic furniture, and advanced
					technology. We want our employees to have the best possible workspaces
					that inspire creativity and innovation. Stay tuned for more details on
					the grand opening events.
				</p>
			</div>
		</main>
	)
}

export default MainContent