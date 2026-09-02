<script lang="ts">
	import { resolve } from '$app/paths';
	import Logo from '$lib/components/shared/Logo.svelte';
	import Button from '$lib/components/ui/button/button.svelte';
	import { ArrowRight, CircleAlert, Eye, EyeOff } from 'lucide-svelte';
	import { onDestroy } from 'svelte';
	import Icon from '@iconify/svelte';
	import { goto } from '$app/navigation';

	interface RegisterData {
		name: string;
		email: string;
		password: string;
		confirmPassword: string;
	}

	interface Props {
		onSubmit?: (data: RegisterData) => void;
	}

	let redirectTimer: ReturnType<typeof setTimeout>;

	let { onSubmit }: Props = $props();

	let showPassword = $state(false);
	let loading = $state(false);
	let success = $state(false);

	let error = $state('');

	let formData = $state<RegisterData>({
		name: '',
		email: '',
		password: '',
		confirmPassword: ''
	});

	function validateForm() {
		error = '';

		if (!formData.name.trim()) {
			error = 'Name is required';
			return false;
		}

		if (!formData.email.trim()) {
			error = 'Email is required';
			return false;
		}

		if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
			error = 'Please enter a valid email';
			return false;
		}

		if (!formData.password) {
			error = 'Password is required';
			return false;
		}

		if (formData.password.length < 3) {
			error = 'Password must be at least 3 characters';
			return false;
		}

		if (!formData.confirmPassword) {
			error = 'Please confirm your password';
			return false;
		}

		if (formData.password !== formData.confirmPassword) {
			error = 'Passwords do not match';
			return false;
		}

		return true;
	}

	async function handleSubmit(event: SubmitEvent) {
		event.preventDefault();

		if (!validateForm()) {
			return;
		}

		loading = true;

		try {
			// const { error: signUpError } = await authClient.signUp.email({
			// 	name: formData.name,
			// 	email: formData.email,
			// 	password: formData.password
			// });

			// if (signUpError) {
			// 	error = signUpError.message || 'Unable to create your account.';
			// 	return;
			// }

			success = true;

			redirectTimer = setTimeout(async () => {
				if (onSubmit) {
					onSubmit(formData);
				} else {
					await goto(resolve('/'));
				}
			}, 1500);
		} catch (err) {
			error = err instanceof Error ? err.message : 'Unable to create your account.';
			success = false;
		} finally {
			loading = false;
		}
	}
	onDestroy(() => {
		if (redirectTimer) {
			clearTimeout(redirectTimer);
		}
	});
	function handleInput(event: Event) {
		const target = event.currentTarget as HTMLInputElement;
		const { name, value } = target;
		error = '';
		formData[name as keyof RegisterData] = value;
	}
</script>

{#if success}
	<section class="flex min-h-screen items-center justify-center px-4">
		<div class="w-[min(100%,440px)] text-center">
			<div
				class="success-icon mx-auto mb-5 flex size-14 items-center justify-center rounded-full bg-[#22c55e1a] text-[#4ade80]"
			>
				<svg
					class="checkmark"
					width="25"
					height="25"
					viewBox="0 0 24 24"
					fill="none"
					stroke="currentColor"
					stroke-width="2.5"
					stroke-linecap="round"
					stroke-linejoin="round"
				>
					<path d="M5 12.5l4.5 4.5L19 7.5" />
				</svg>
			</div>

			<h1 class="text-[26px] font-semibold tracking-[-0.04em]">Account created</h1>

			<p class="mt-2 text-[13px] text-muted-foreground">Your account is ready to use.</p>

			<p class="mt-6 text-[11px] text-muted-foreground">Redirecting to workspace...</p>
		</div>
	</section>
{:else}
	<section
		class="w-[min(100%,440px)] rounded-[12px] border border-border bg-[#1b1d1ef0] px-[clamp(40px,4vw,42px)] py-10.5 shadow-[0_22px_80px_#00000047] md:w-full md:border-0 md:bg-transparent md:p-0 md:shadow-none"
	>
		<div class="text-center">
			<Logo compact={false} />
			<div class="mb-9.5"></div>
			<h1 class="text-[30px]">Create account</h1>

			<p class="m-[10px_0_30px] text-[13px] leading-[1.6] text-muted-foreground">
				Join Forge and start collaborating with your team.
			</p>
		</div>

		<form class="grid gap-4.5" onsubmit={handleSubmit}>
			<div class="form-group">
				<label for="name">Full name</label>

				<input
					autocomplete="name"
					id="name"
					name="name"
					type="text"
					placeholder="Alex Morgan"
					value={formData.name}
					oninput={handleInput}
				/>
			</div>

			<div class="form-group">
				<label for="email">Email address</label>

				<input
					id="email"
					autocomplete="email"
					name="email"
					type="email"
					placeholder="you@example.com"
					value={formData.email}
					oninput={handleInput}
				/>
			</div>

			<div class="form-group">
				<label for="password">Password</label>

				<div class="password-input-wrapper">
					<input
						id="password"
						name="password"
						autocomplete="new-password"
						type={showPassword ? 'text' : 'password'}
						placeholder="••••••••"
						value={formData.password}
						oninput={handleInput}
					/>

					<button
						tabindex="-1"
						type="button"
						class="password-toggle"
						aria-label={showPassword ? 'Hide password' : 'Show password'}
						onclick={() => (showPassword = !showPassword)}
					>
						{#if showPassword}
							<EyeOff size={16} />
						{:else}
							<Eye size={16} />
						{/if}
					</button>
				</div>
			</div>

			<div class="form-group">
				<label for="confirmPassword"> Confirm password </label>

				<div class="password-input-wrapper">
					<input
						id="confirmPassword"
						name="confirmPassword"
						autocomplete="new-password"
						type={showPassword ? 'text' : 'password'}
						placeholder="••••••••"
						value={formData.confirmPassword}
						oninput={handleInput}
					/>

					<button
						tabindex="-1"
						type="button"
						class="password-toggle"
						aria-label={showPassword ? 'Hide password' : 'Show password'}
						onclick={() => (showPassword = !showPassword)}
					>
						{#if showPassword}
							<EyeOff size={16} />
						{:else}
							<Eye size={16} />
						{/if}
					</button>
				</div>
			</div>

			{#if error}
				<span
					class="flex items-center gap-2 border-l-2 border-[#e28b7d] pl-2 text-[16px] leading-4 text-[#d6a19a]"
				>
					<CircleAlert size={12} strokeWidth={1.8} />
					{error}
				</span>
			{/if}

			<Button
				type="submit"
				class="mt-5 flex h-12 w-full cursor-pointer items-center justify-center gap-2 rounded-[7px] border-0 p-[12px_16px] text-[13px] font-bold"
				disabled={loading}
			>
				{loading ? 'Please wait...' : 'Create account'}

				{#if !loading}
					<ArrowRight size={16} />
				{/if}
			</Button>
		</form>

		<div class="mt-2.75 grid gap-3.25 text-center text-[11px] text-muted-foreground">
			<p>
				Already have an account?

				<a href={resolve('/login')} class="text-primary no-underline"> Sign in </a>
			</p>
		</div>
		<div class="social-section">
			<div class="divider">
				<span>OR CONTINUE WITH</span>
			</div>

			<div class="social-buttons">
				<button type="button" class="social-button">
					<Icon icon="simple-icons:github" width="16" color="currentColor" />
					GitHub
				</button>

				<button type="button" class="social-button">
					<Icon icon="logos:google-icon" width="20" />

					Google
				</button>
			</div>
		</div>
	</section>
{/if}

<style>
	.form-group {
		display: grid;
		gap: 7px;
	}
	.form-group label {
		font-size: 12px;
		color: #c5c8c4;
	}

	.form-group input {
		border-radius: 7px;
		background-color: #151718;
		padding: 11px 12px;
		font-size: 13px;
		color: var(--foreground);
		outline: none;
		width: 100%;
	}
	.form-group input:focus {
		outline: 1px solid var(--color-primary);
		box-shadow: 0 0 12px rgba(237, 121, 95, 0.2);
	}
	.password-input-wrapper {
		position: relative;
		display: block;
	}
	.password-input-wrapper input {
		padding-right: 40px;
	}
	.password-toggle {
		color: var(--muted-foreground);
		cursor: pointer;
		background: none;
		border: 0;
		border-radius: 5px;
		padding: 7px;
		position: absolute;
		top: 5px;
		right: 5px;
	}

	.success-icon {
		animation: success-pop 0.35s ease-out;
	}

	.checkmark {
		animation: check-draw 0.4s 0.15s ease-out both;
	}
	.social-buttons {
		display: grid;
		grid-template-columns: 1fr 1fr;
		gap: 9px;
		margin-top: 13px;
	}

	.social-button {
		display: flex;
		height: 40px;
		align-items: center;
		justify-content: center;
		gap: 9px;
		border: 1px solid rgba(255, 255, 255, 0.07);
		border-radius: 7px;
		background: rgba(255, 255, 255, 0.025);
		color: rgba(255, 255, 255, 0.65);
		font-size: 11px;
		cursor: pointer;
		transition:
			border-color 180ms ease,
			background 180ms ease,
			transform 180ms ease;
	}

	.social-button:hover {
		border-color: rgba(255, 255, 255, 0.13);
		background: rgba(255, 255, 255, 0.045);
		transform: translateY(-1px);
	}

	.social-section {
		margin-top: 26px;
	}

	.divider {
		display: flex;
		align-items: center;
		gap: 12px;
		color: rgba(255, 255, 255, 0.2);
		font-size: 8px;
		letter-spacing: 0.12em;
	}

	.divider::before,
	.divider::after {
		content: '';
		height: 1px;
		flex: 1;
		background: rgba(255, 255, 255, 0.06);
	}

	@keyframes success-pop {
		0% {
			opacity: 0;
			transform: scale(0.8);
		}

		70% {
			transform: scale(1.05);
		}

		100% {
			opacity: 1;
			transform: scale(1);
		}
	}

	@keyframes check-draw {
		0% {
			opacity: 0;
			transform: scale(0.7);
		}

		100% {
			opacity: 1;
			transform: scale(1);
		}
	}
</style>
