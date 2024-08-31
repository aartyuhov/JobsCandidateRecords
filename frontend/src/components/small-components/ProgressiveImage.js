import React, { useState, useEffect } from 'react'

const ProgressiveImage = ({ src, alt, className }) => {
	const [imageSrc, setImageSrc] = useState('')
	const [imageRef, setImageRef] = useState()

	useEffect(() => {
		let observer
		let didCancel = false

		if (imageRef && imageSrc !== src) {
			if (IntersectionObserver) {
				observer = new IntersectionObserver(
					entries => {
						entries.forEach(entry => {
							if (
								!didCancel &&
								(entry.intersectionRatio > 0 || entry.isIntersecting)
							) {
								setImageSrc(src)
								observer.unobserve(imageRef)
							}
						})
					},
					{
						threshold: 0.01,
						rootMargin: '75%',
					}
				)
				observer.observe(imageRef)
			} else {
				setImageSrc(src)
			}
		}
		return () => {
			didCancel = true
			if (observer && observer.unobserve) {
				observer.unobserve(imageRef)
			}
		}
	}, [src, imageSrc, imageRef])

	return (
		<img
			className={`progressive-image ${className || ''}`}
			ref={setImageRef}
			src={imageSrc}
			alt={alt}
		/>
	)
}

export default ProgressiveImage