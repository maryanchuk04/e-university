/** @type {import('tailwindcss').Config} */
module.exports = {
	content: ['./src/**/*.{html,ts}'],
	theme: {
		extend: {
			screens: {
				'2xl': '1536px',
				xl: '1280px',
				lg: '1024px',
				md: '768px',
				sm: '576px',
				xs: '0px',
			},
		},
	},
	plugins: [],
	corePlugins: { preflight: false },
};
