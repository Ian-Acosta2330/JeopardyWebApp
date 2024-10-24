<script>
	import Component2 from './Component2.svelte';
	let name = 'world';
</script>

<style> 

	:global(h1) {
		color: rebeccapurple;
		animation: zoom 2s infinite;
	}
	
	p {
		font-size: 40px;
	}
	/* @keyframes zoom {
		0%, 100% {transform: scale(3)}
		50% {transform: scale(0.5)}
	}
 */
</style>

<Component2 />

<h1>Introduction to Svelte Components</h1>
<p> What's up</p>
