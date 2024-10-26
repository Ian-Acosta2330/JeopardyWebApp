<title>Game Page</title>

<script>
    import Taskbar from '/src/components/taskbar.svelte';
    const items = [
        {name: 'Home', href: '/routes'},
        {name: 'Game', href: '/game/'},
        {name: 'Profile', href: '/'},
        {name: 'Settings', href: '/'},
        {name: 'Logout', href:'/'}
    ];
    
</script>

<Taskbar {items}/>

<h1 class="heading"> Game Portion </h1>

<body>

   

</body>

