<script lang="ts">
  let { data } = $props();
  let { numberOfdelayedTrains, numberOfAffectedStops } = data;
</script>

<div class="h-screen flex flex-col items-center justify-center text-center">
  {#if numberOfdelayedTrains > 0}
    <h1 class="text-5xl font-bold">Ja</h1>
    <p>Det står nå folk på {numberOfAffectedStops} stasjoner og venter på tog som er forsinket.
    </p>
  {:else}
    <h1 class="text-5xl font-bold">🎉 Nei 🎉</h1>
  {/if}
</div>