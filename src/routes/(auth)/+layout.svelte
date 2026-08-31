<script>
	import Logo from '$lib/components/shared/Logo.svelte';
	let { children } = $props();
</script>

<div class="register-page relative min-h-dvh overflow-hidden bg-[#0b0d0e] text-foreground">
	<!-- Technical grid -->
	<div class="grid-background"></div>

	<div class="particles pointer-events-none">
		<span></span>
		<span></span>
		<span></span>
		<span></span>
		<span></span>
		<span></span>
		<span></span>
		<span></span>
	</div>

	<div class="relative z-10 grid min-h-dvh lg:grid-cols-[1.15fr_0.85fr]">
		<!-- =========================================
		     HERO / FORGE SIDE
		========================================= -->
		<section class="hero-side relative hidden overflow-hidden md:flex">
			<!-- Project graph -->
			<div class="project-graph">
				<div class="graph-node graph-node-1"></div>
				<div class="graph-node graph-node-2"></div>
				<div class="graph-node graph-node-3"></div>
				<div class="graph-node graph-node-4"></div>
				<div class="graph-node graph-node-5"></div>

				<div class="graph-line graph-line-1"></div>
				<div class="graph-line graph-line-2"></div>
				<div class="graph-line graph-line-3"></div>
				<div class="graph-line graph-line-4"></div>
			</div>

			<!-- Hero content -->
			<div class="hero-content">
				<div class="hero-logo">
					<div class="hero-logo-glow"></div>
					<Logo compact />
				</div>

				<div class="hero-copy">
					<div class="eyebrow">
						<span class="eyebrow-dot"></span>
						WORKSPACE MANAGEMENT
					</div>

					<h1>
						Shape your
						<span>workspace.</span>
					</h1>

					<p>
						Forge gives your team a focused place to plan projects, organize work, and ship
						together.
					</p>
				</div>

				<!-- Small product features -->
				<div class="hero-features">
					<div class="feature">
						<div class="feature-icon">01</div>
						<div>
							<strong>Organize</strong>
							<span>Keep every project structured.</span>
						</div>
					</div>

					<div class="feature">
						<div class="feature-icon">02</div>
						<div>
							<strong>Collaborate</strong>
							<span>Bring your team into one workspace.</span>
						</div>
					</div>

					<div class="feature">
						<div class="feature-icon">03</div>
						<div>
							<strong>Ship</strong>
							<span>Turn ideas into finished work.</span>
						</div>
					</div>
				</div>
			</div>

			<div class="hero-footer">
				<span>BUILD · SHIP · COLLABORATE</span>

				<div class="footer-line"></div>

				<span>FORGE / 01</span>
			</div>
		</section>

		<!-- =========================================
		     AUTH SIDE
		========================================= -->
		<section
			class="auth-side relative flex min-h-dvh items-center justify-center px-5 py-10 sm:px-8 lg:min-h-0 lg:border-l lg:border-white/4.5"
		>
			<div class="auth-content w-full max-w-110">
				{@render children()}
			</div>
		</section>
	</div>
</div>

<style>
	.register-page {
		isolation: isolate;
	}

	/* ================================
	   BACKGROUND
	================================ */

	.grid-background {
		position: absolute;
		inset: 0;
		pointer-events: none;
		opacity: 0.45;

		background-image:
			linear-gradient(rgba(255, 255, 255, 0.018) 1px, transparent 1px),
			linear-gradient(90deg, rgba(255, 255, 255, 0.018) 1px, transparent 1px);

		background-size: 52px 52px;

		mask-image: radial-gradient(
			ellipse at 30% 50%,
			black 0%,
			rgba(0, 0, 0, 0.5) 40%,
			transparent 75%
		);
	}

	/* ================================
	   HERO
	================================ */

	.hero-side {
		border-right: 1px solid rgba(255, 255, 255, 0.025);
	}

	.hero-content {
		position: relative;
		z-index: 5;
		display: flex;
		width: 100%;
		flex-direction: column;
		justify-content: center;
		padding: clamp(50px, 8vw, 120px);
	}

	.hero-logo {
		position: absolute;
		top: 42px;
		left: clamp(50px, 8vw, 120px);
		z-index: 6;
		opacity: 0;
		animation: hero-enter 0.8s 0.75s cubic-bezier(0.16, 1, 0.3, 1) both;
	}

	/* Soft glow seated behind the logo so it reads as part of the forge light
	   instead of competing with the grid + core shapes behind it */
	.hero-logo-glow {
		position: absolute;
		top: 50%;
		left: -14px;
		width: 90px;
		height: 90px;
		transform: translateY(-50%);
		background: radial-gradient(circle, rgba(237, 121, 95, 0.16) 0%, transparent 70%);
		filter: blur(10px);
		pointer-events: none;
		z-index: -1;
	}

	.hero-copy {
		max-width: 540px;
		opacity: 0;
		animation: hero-enter 0.5s 0.6s cubic-bezier(0.16, 1, 0.3, 1) both;
	}

	.eyebrow {
		display: flex;
		align-items: center;
		gap: 8px;
		font-size: 9px;
		font-weight: 600;
		letter-spacing: 0.18em;
		color: rgba(255, 255, 255, 0.32);
	}

	.eyebrow-dot {
		width: 5px;
		height: 5px;
		border-radius: 50%;
		background: #ed795f;
		box-shadow: 0 0 10px rgba(237, 121, 95, 0.7);
	}

	.hero-copy h1 {
		margin-top: 20px;
		font-size: clamp(48px, 5vw, 78px);
		font-weight: 600;
		line-height: 0.98;
		letter-spacing: -0.055em;
	}

	.hero-copy h1 span {
		display: block;
		color: #ed795f;
	}

	.hero-copy p {
		max-width: 440px;
		margin-top: 26px;
		font-size: 14px;
		line-height: 1.8;
		color: rgba(255, 255, 255, 0.4);
	}

	/* ================================
	   FEATURES
	================================ */

	.hero-features {
		display: grid;
		grid-template-columns: repeat(3, 1fr);
		max-width: 600px;
		margin-top: 70px;
		border-top: 1px solid rgba(255, 255, 255, 0.06);
		opacity: 0;
		animation: hero-enter 0.9s 1.15s cubic-bezier(0.16, 1, 0.3, 1) both;
	}

	.feature {
		display: flex;
		gap: 12px;
		padding-top: 18px;
	}

	.feature + .feature {
		padding-left: 20px;
		border-left: 1px solid rgba(255, 255, 255, 0.06);
	}

	.feature-icon {
		font-size: 9px;
		color: #ed795f;
		opacity: 0.7;
	}

	.feature strong {
		display: block;
		font-size: 11px;
		font-weight: 600;
	}

	.feature span {
		display: block;
		margin-top: 4px;
		font-size: 9px;
		line-height: 1.5;
		color: rgba(255, 255, 255, 0.25);
	}

	/* ================================
	   GRAPH
	================================ */

	.project-graph {
		position: absolute;
		inset: 0;
		opacity: 0;
		animation: hero-enter 1s 0.9s cubic-bezier(0.16, 1, 0.3, 1) both;
	}

	.graph-node {
		position: absolute;
		width: 5px;
		height: 5px;
		border-radius: 50%;
		background: #ed795f;
		box-shadow: 0 0 14px rgba(237, 121, 95, 0.7);
		animation: node-pulse 4s ease-in-out infinite;
	}

	.graph-node-1 {
		top: 24%;
		left: 18%;
	}

	.graph-node-2 {
		top: 34%;
		left: 30%;
		animation-delay: 0.7s;
	}

	.graph-node-3 {
		top: 28%;
		left: 48%;
		animation-delay: 1.3s;
	}

	.graph-node-4 {
		bottom: 31%;
		left: 23%;
		animation-delay: 1.9s;
	}

	.graph-node-5 {
		bottom: 23%;
		left: 43%;
		animation-delay: 2.5s;
	}

	.graph-line {
		position: absolute;
		height: 1px;
		transform-origin: left;
		background: linear-gradient(90deg, rgba(237, 121, 95, 0.18), rgba(237, 121, 95, 0.02));
	}

	.graph-line-1 {
		top: 24%;
		left: 18%;
		width: 160px;
		transform: rotate(27deg);
	}

	.graph-line-2 {
		top: 34%;
		left: 30%;
		width: 190px;
		transform: rotate(-14deg);
	}

	.graph-line-3 {
		top: 28%;
		left: 48%;
		width: 150px;
		transform: rotate(110deg);
	}

	.graph-line-4 {
		bottom: 31%;
		left: 23%;
		width: 210px;
		transform: rotate(-20deg);
	}

	/* ================================
	   FOOTER
	================================ */

	.hero-footer {
		position: absolute;
		right: clamp(50px, 8vw, 120px);
		bottom: 36px;
		left: clamp(50px, 8vw, 120px);
		display: flex;
		align-items: center;
		gap: 14px;
		font-size: 8px;
		letter-spacing: 0.2em;
		color: rgba(255, 255, 255, 0.15);
		opacity: 0;
		animation: hero-enter 0.8s 1.3s cubic-bezier(0.16, 1, 0.3, 1) both;
	}

	.footer-line {
		height: 1px;
		flex: 1;
		background: rgba(255, 255, 255, 0.05);
	}

	/* ================================
	   AUTH
	================================ */

	.auth-side {
		background: rgba(10, 12, 13, 0.55);
		backdrop-filter: blur(20px);
	}

	/* .auth-content {
		animation: form-enter 0.75s 0.12s cubic-bezier(0.16, 1, 0.3, 1) both;
	} */

	/* ================================
	   PARTICLES
	================================ */

	.particles span {
		position: absolute;
		z-index: 2;
		width: 2px;
		height: 2px;
		border-radius: 50%;
		background: #ed795f;
		opacity: 0;
		animation: particle-float 6s ease-in-out infinite;
	}

	.particles span:nth-child(1) {
		left: 15%;
		top: 60%;
	}

	.particles span:nth-child(2) {
		left: 28%;
		top: 72%;
		animation-delay: 1s;
	}

	.particles span:nth-child(3) {
		left: 41%;
		top: 20%;
		animation-delay: 2s;
	}

	.particles span:nth-child(4) {
		left: 55%;
		top: 63%;
		animation-delay: 2.8s;
	}

	.particles span:nth-child(5) {
		left: 70%;
		top: 31%;
		animation-delay: 1.4s;
	}

	.particles span:nth-child(6) {
		left: 80%;
		top: 70%;
		animation-delay: 3.5s;
	}

	.particles span:nth-child(7) {
		left: 90%;
		top: 24%;
		animation-delay: 4s;
	}

	.particles span:nth-child(8) {
		left: 9%;
		top: 35%;
		animation-delay: 4.5s;
	}

	/* ================================
	   ANIMATIONS
	================================ */

	@keyframes hero-enter {
		from {
			opacity: 0;
			transform: translateX(-35px) rotateY(5deg);
		}

		to {
			opacity: 1;
			transform: translateX(0) rotateY(0);
		}
	}

	@keyframes form-enter {
		from {
			opacity: 0;
			transform: translateX(30px);
		}

		to {
			opacity: 1;
			transform: translateX(0);
		}
	}

	@keyframes forge-materialize {
		from {
			opacity: 0;
			transform: translateY(-50%) rotate(-18deg) scale(0.85);
		}

		to {
			opacity: 0.5;
			transform: translateY(-50%) rotate(-18deg) scale(1);
		}
	}

	@keyframes ring-rotate {
		from {
			transform: scale(1) rotate(0deg);
		}

		to {
			transform: scale(1) rotate(360deg);
		}
	}

	@keyframes ring-rotate-reverse {
		from {
			transform: scale(0.72) rotate(360deg);
		}

		to {
			transform: scale(0.72) rotate(0deg);
		}
	}

	@keyframes node-pulse {
		0%,
		100% {
			opacity: 0.25;
			transform: scale(1);
		}

		50% {
			opacity: 0.8;
			transform: scale(1.5);
		}
	}

	@keyframes particle-float {
		0% {
			opacity: 0;
			transform: translateY(15px);
		}

		25% {
			opacity: 0.45;
		}

		70% {
			opacity: 0.15;
		}

		100% {
			opacity: 0;
			transform: translateY(-40px);
		}
	}

	@keyframes ambient-breathe {
		0%,
		100% {
			opacity: 1;
			transform: scale(1);
		}

		50% {
			opacity: 1.6;
			transform: scale(1.08);
		}
	}

	@keyframes impact-pulse {
		0% {
			opacity: 0;
			transform: scale(0.3);
		}

		30% {
			opacity: 1;
			transform: scale(1.5);
		}

		100% {
			opacity: 0;
			transform: scale(2.3);
		}
	}

	@keyframes shockwave-expand {
		0% {
			opacity: 0.85;
			transform: translate(-50%, -50%) scale(0.5);
		}

		55% {
			opacity: 0.3;
		}

		100% {
			opacity: 0;
			transform: translate(-50%, -50%) scale(4.4);
		}
	}

	@keyframes shard-fly {
		0% {
			opacity: 0;
			transform: translate(-50%, -50%) translate(0, 0) rotate(0deg) scale(0.6);
		}

		18% {
			opacity: 1;
		}

		100% {
			opacity: 0;
			transform: translate(-50%, -50%) translate(var(--tx), var(--ty)) rotate(var(--rot))
				scale(0.25);
		}
	}

	@media (prefers-reduced-motion: reduce) {
		.hero-content,
		.hero-logo,
		.hero-copy,
		.hero-features,
		.hero-footer,
		.auth-content,
		.graph-node,
		.project-graph,
		.particles span {
			animation: none;
			opacity: 1;
		}
	}
	@media (prefers-reduced-motion: reduce) {
		.auth-content {
			animation: none;
		}
	}

	@media (max-width: 1023px) {
		.grid-background {
			mask-image: radial-gradient(ellipse at center, black 0%, transparent 75%);
		}
	}
</style>
