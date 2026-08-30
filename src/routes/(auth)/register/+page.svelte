<script>
	import RegisterForm from '$lib/authentication/components/RegisterForm.svelte';
</script>

<main
	class="register-page relative flex min-h-dvh items-center justify-center overflow-hidden bg-[#101213] px-4.5 py-8"
>
	<!-- Ambient forge glow -->
	<div
		class="pointer-events-none absolute top-1/2 left-1/2 size-175 -translate-x-1/2 -translate-y-1/2 rounded-full bg-[#ed795f]/2.5 blur-[140px]"
	></div>

	<!-- Project grid -->
	<div class="project-grid pointer-events-none absolute inset-0"></div>

	<!-- Forge atmosphere -->
	<div class="forge-atmosphere pointer-events-none absolute inset-0">
		<span class="spark spark-1"></span>
		<span class="spark spark-2"></span>
		<span class="spark spark-3"></span>
		<span class="spark spark-4"></span>
	</div>

	<!-- Decorative project graph -->
	<div class="project-graph pointer-events-none absolute inset-0">
		<div class="node node-1"></div>
		<div class="node node-2"></div>
		<div class="node node-3"></div>
		<div class="node node-4"></div>

		<div class="connection connection-1"></div>
		<div class="connection connection-2"></div>
		<div class="connection connection-3"></div>
	</div>

	<RegisterForm />

	<!-- Bottom brand statement -->
	<div
		class="pointer-events-none absolute bottom-6 left-1/2 -translate-x-1/2 text-[9px] tracking-[0.28em] whitespace-nowrap text-white/12"
	>
		BUILD · SHIP · COLLABORATE
	</div>
</main>

<style>
	.project-grid {
		background-image:
			linear-gradient(rgba(255, 255, 255, 0.018) 1px, transparent 1px),
			linear-gradient(90deg, rgba(255, 255, 255, 0.018) 1px, transparent 1px);
		background-size: 48px 48px;

		mask-image: radial-gradient(ellipse at center, black 0%, transparent 72%);
	}

	/* ------------------------------
	   Project graph
	-------------------------------- */

	.project-graph {
		opacity: 0.35;
	}

	.node {
		position: absolute;
		width: 6px;
		height: 6px;
		border-radius: 50%;
		background: #ed795f;
		box-shadow: 0 0 14px rgba(237, 121, 95, 0.4);
		animation: node-pulse 4s ease-in-out infinite;
	}

	.node-1 {
		left: 17%;
		top: 27%;
	}

	.node-2 {
		left: 27%;
		top: 39%;
		animation-delay: 0.8s;
	}

	.node-3 {
		right: 22%;
		top: 31%;
		animation-delay: 1.4s;
	}

	.node-4 {
		right: 15%;
		bottom: 29%;
		animation-delay: 2s;
	}

	.connection {
		position: absolute;
		height: 1px;
		transform-origin: left center;
		background: linear-gradient(90deg, rgba(237, 121, 95, 0.25), transparent);
	}

	.connection-1 {
		left: 17%;
		top: 27%;
		width: 150px;
		transform: rotate(31deg);
	}

	.connection-2 {
		left: 27%;
		top: 39%;
		width: 190px;
		transform: rotate(-22deg);
	}

	.connection-3 {
		right: 18%;
		top: 32%;
		width: 150px;
		transform: rotate(115deg);
	}

	/* ------------------------------
	   Forge sparks
	-------------------------------- */

	.spark {
		position: absolute;
		width: 2px;
		height: 2px;
		border-radius: 50%;
		background: #ed795f;
		opacity: 0;
		animation: spark-float 5s ease-in-out infinite;
	}

	.spark-1 {
		left: 20%;
		top: 62%;
		animation-delay: 0s;
	}

	.spark-2 {
		left: 75%;
		top: 25%;
		animation-delay: 1.5s;
	}

	.spark-3 {
		left: 82%;
		top: 68%;
		animation-delay: 2.8s;
	}

	.spark-4 {
		left: 13%;
		top: 35%;
		animation-delay: 3.5s;
	}

	@keyframes node-pulse {
		0%,
		100% {
			opacity: 0.25;
			transform: scale(1);
		}

		50% {
			opacity: 0.7;
			transform: scale(1.4);
		}
	}

	@keyframes spark-float {
		0% {
			opacity: 0;
			transform: translateY(12px) scale(0.5);
		}

		30% {
			opacity: 0.5;
		}

		70% {
			opacity: 0.25;
		}

		100% {
			opacity: 0;
			transform: translateY(-30px) scale(1);
		}
	}

	@media (prefers-reduced-motion: reduce) {
		.node,
		.spark {
			animation: none;
		}
	}
</style>
