<script lang="ts">
  let { data } = $props();
  let { numberOfdelayedTrains, timeSinceLastDelayMinutes } = data;
</script>

<div class="h-screen flex flex-col items-center justify-center text-center">
  {#if numberOfdelayedTrains > 0}
    <h1 class="text-5xl font-bold">Ja</h1>
    <p>Det er {numberOfdelayedTrains} forsinkede tog nå.</p>
  {:else}
    <h1 class="text-5xl font-bold">🎉 Nei 🎉</h1>
    <p>Det er {timeSinceLastDelayMinutes} minutter siden siste forsinkelse.</p>
  {/if}
</div>