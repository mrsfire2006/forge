<script lang="ts">
	// type Product = {
	// 	id: number;
	// 	title: string;
	// 	description: string;
	// 	category: string;
	// };

	// type ProductsResponse = {
	// 	products: Product[];
	// 	total: number;
	// 	skip: number;
	// 	limit: number;
	// };
	// const queryClient = useQueryClient();
	// const queryProducts = createInfiniteQuery(() => ({
	// 	queryKey: ['products'],
	// 	staleTime: 60 * 1000,
	// 	initialPageParam: 0,
	// 	queryFn: async ({ pageParam }): Promise<ProductsResponse> => {
	// 		const response = await fetch(`https://dummyjson.com/products?limit=5&skip=${pageParam}`);

	// 		if (!response.ok) {
	// 			throw new Error('Failed to fetch products');
	// 		}

	// 		return response.json();
	// 	},
	// 	getNextPageParam: (lastPage) => {
	// 		const nextSkip = lastPage.skip + lastPage.limit;

	// 		return nextSkip < lastPage.total ? nextSkip : undefined;
	// 	}
	// }));

	// const handleDelete = (id: number) => {
	// 	queryClient.setQueryData<InfiniteData<ProductsResponse>>(['products'], (oldData) => {
	// 		if (!oldData) return;
	// 		return {
	// 			...oldData,
	// 			pages: oldData.pages.map((page) => {
	// 				return {
	// 					...page,
	// 					products: page.products.filter((product) => product.id !== id)
	// 				};
	// 			})
	// 		};
	// 	});
	// };
</script>

<main>
	<!-- <section class="header">
		<div>
			<p class="eyebrow">CATALOG</p>
			<h1>Products</h1>
			<p class="subtitle">Browse and explore your product collection.</p>
		</div>

		<div class="stats">
			<div class="stat">
				<span>Loaded</span>
				<strong>{queryProducts.data?.pages.flatMap((page) => page.products).length}</strong>
			</div>
		</div>
	</section>

	<section class="table-card">
		<div class="table-wrapper">
			<table>
				<thead>
					<tr>
						<th class="id-column">ID</th>
						<th>Product</th>
						<th>Description</th>
					</tr>
				</thead>

				<tbody>
					{#if queryProducts.isPending}
						<tr>
							<td colspan="3">
								<div class="flex h-44 items-center justify-center">
									<span
										class="size-7 animate-spin rounded-full border-3 border-zinc-700 border-t-zinc-100"
									></span>
								</div>
							</td>
						</tr>
					{:else if queryProducts.isError}
						<tr>
							<td colspan="3" class="error"> Failed to load products. </td>
						</tr>
					{:else}
						{#each queryProducts.data?.pages.flatMap((page) => page.products) ?? [] as product (product.id)}
							<tr>
								<td>
									<span class="id">{product.id}</span>
								</td>

								<td>
									<div class="product">
										<div class="avatar">
											{product.title.charAt(0)}
										</div>

										<div>
											<p class="title">{product.title}</p>
											<span class="category">
												{product.category}
											</span>
										</div>
									</div>
								</td>

								<td>
									<p class="description">
										{product.description}
									</p>
								</td>

								<td class="px-4 py-3 text-right">
									<button
										type="button"
										onclick={() => handleDelte(product.id)}
										aria-label="Delete product"
										title="Delete product"
										class="inline-flex size-8 items-center justify-center rounded-md text-muted-foreground transition-colors hover:bg-red-50 hover:text-red-600 focus-visible:ring-2 focus-visible:ring-red-500/30 focus-visible:outline-none dark:hover:bg-red-950/40 dark:hover:text-red-400"
									>
										<Trash2 size={16} strokeWidth={1.8} />
									</button>
								</td>
							</tr>
						{/each}
					{/if}
				</tbody>
			</table>
		</div>

		<div class="footer">
			<span>
				Showing <strong>{queryProducts.data?.pages.flatMap((page) => page.products).length}</strong> products
			</span>

			{#if queryProducts.hasNextPage}
				<button
					class="load-button"
					disabled={queryProducts.isFetchingNextPage}
					onclick={() => queryProducts.fetchNextPage()}
				>
					{#if queryProducts.isFetchingNextPage}
						<span class="spinner"></span>
						Loading...
					{:else}
						Load more
						<span class="arrow">↓</span>
					{/if}
				</button>
			{:else}
				<span class="end">All products loaded</span>
			{/if}
		</div>
	</section> -->
</main>

<!-- <style>
	:global(*) {
		box-sizing: border-box;
	}

	:global(body) {
		margin: 0;
		background: #09090b;
		color: #fafafa;
		font-family:
			Inter,
			ui-sans-serif,
			system-ui,
			-apple-system,
			BlinkMacSystemFont,
			'Segoe UI',
			sans-serif;
	}

	.page {
		min-height: 100vh;
		padding: 48px;
		background:
			radial-gradient(circle at 80% 0%, rgba(255, 255, 255, 0.045), transparent 30%), #09090b;
	}

	.header {
		max-width: 1200px;
		margin: 0 auto 28px;
		display: flex;
		align-items: flex-end;
		justify-content: space-between;
		gap: 24px;
	}

	.eyebrow {
		margin: 0 0 8px;
		font-size: 11px;
		font-weight: 700;
		letter-spacing: 0.14em;
		color: #71717a;
	}

	h1 {
		margin: 0;
		font-size: 32px;
		font-weight: 650;
		letter-spacing: -0.035em;
	}

	.subtitle {
		margin: 8px 0 0;
		color: #71717a;
		font-size: 14px;
	}

	.stats {
		display: flex;
	}

	.stat {
		padding: 12px 18px;
		border: 1px solid #27272a;
		border-radius: 10px;
		background: rgba(24, 24, 27, 0.65);
	}

	.stat span {
		display: block;
		margin-bottom: 3px;
		font-size: 11px;
		color: #71717a;
	}

	.stat strong {
		font-size: 18px;
		font-weight: 600;
	}

	.table-card {
		max-width: 1200px;
		margin: 0 auto;
		overflow: hidden;
		border: 1px solid #27272a;
		border-radius: 14px;
		background: #111113;
		box-shadow:
			0 20px 60px rgba(0, 0, 0, 0.25),
			0 0 0 1px rgba(255, 255, 255, 0.01);
	}

	.table-wrapper {
		overflow-x: auto;
	}

	table {
		width: 100%;
		border-collapse: collapse;
	}

	thead {
		background: #18181b;
	}

	th {
		height: 46px;
		padding: 0 20px;
		border-bottom: 1px solid #27272a;
		color: #71717a;
		font-size: 11px;
		font-weight: 600;
		letter-spacing: 0.04em;
		text-align: left;
		text-transform: uppercase;
	}

	td {
		padding: 17px 20px;
		border-bottom: 1px solid #1f1f22;
		vertical-align: middle;
	}

	tbody tr {
		transition: background 0.15s ease;
	}

	tbody tr:hover {
		background: #18181b;
	}

	tbody tr:last-child td {
		border-bottom: none;
	}

	.id-column {
		width: 80px;
	}

	.id {
		display: inline-flex;
		align-items: center;
		justify-content: center;
		min-width: 32px;
		height: 26px;
		padding: 0 8px;
		border: 1px solid #27272a;
		border-radius: 6px;
		background: #18181b;
		color: #a1a1aa;
		font-family: monospace;
		font-size: 11px;
	}

	.product {
		display: flex;
		align-items: center;
		gap: 12px;
		min-width: 220px;
	}

	.avatar {
		display: flex;
		align-items: center;
		justify-content: center;
		width: 34px;
		height: 34px;
		flex-shrink: 0;
		border: 1px solid #3f3f46;
		border-radius: 8px;
		background: #27272a;
		color: #d4d4d8;
		font-size: 13px;
		font-weight: 600;
	}

	.title {
		margin: 0 0 3px;
		color: #e4e4e7;
		font-size: 14px;
		font-weight: 550;
	}

	.category {
		color: #71717a;
		font-size: 11px;
	}

	.description {
		max-width: 650px;
		margin: 0;
		overflow: hidden;
		color: #a1a1aa;
		font-size: 13px;
		line-height: 1.5;
		text-overflow: ellipsis;
		white-space: nowrap;
	}

	.footer {
		display: flex;
		align-items: center;
		justify-content: space-between;
		padding: 16px 20px;
		border-top: 1px solid #27272a;
		color: #71717a;
		font-size: 12px;
	}

	.footer strong {
		color: #d4d4d8;
		font-weight: 500;
	}

	.load-button {
		display: inline-flex;
		align-items: center;
		gap: 8px;
		height: 36px;
		padding: 0 14px;
		border: 1px solid #3f3f46;
		border-radius: 8px;
		background: #fafafa;
		color: #18181b;
		font-size: 13px;
		font-weight: 600;
		cursor: pointer;
		transition:
			background 0.15s ease,
			transform 0.15s ease;
	}

	.load-button:hover:not(:disabled) {
		background: #e4e4e7;
		transform: translateY(-1px);
	}

	.load-button:disabled {
		cursor: wait;
		opacity: 0.6;
	}

	.arrow {
		font-size: 15px;
	}

	.spinner {
		width: 13px;
		height: 13px;
		border: 2px solid #a1a1aa;
		border-top-color: #18181b;
		border-radius: 50%;
		animation: spin 0.7s linear infinite;
	}

	.end {
		color: #52525b;
	}

	@keyframes spin {
		to {
			transform: rotate(360deg);
		}
	}

	@media (max-width: 700px) {
		.page {
			padding: 24px 16px;
		}

		.header {
			align-items: flex-start;
			flex-direction: column;
		}

		h1 {
			font-size: 26px;
		}

		.table-card {
			border-radius: 10px;
		}

		.footer {
			align-items: flex-start;
			flex-direction: column;
			gap: 14px;
		}

		.load-button {
			width: 100%;
			justify-content: center;
		}
	}
</style> -->
