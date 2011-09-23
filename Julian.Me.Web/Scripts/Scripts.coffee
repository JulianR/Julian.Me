$.fn.createTabs = () ->
  $div = this

  $div.addClass 'tab-container'

  sections = this.children 'section'

  $div.before '<ul class="tab-header"></ul>'
  
  $ul = $('ul.tab-header').first()

  tabId = 1

  first = true

  sections.each () ->
    $section = $ this

    title = $section.attr 'title';

    $ul.append "<li><h2><a id=\"tab-header-#{tabId}\" href=\"#\">#{title}</a></h2></li>"

    $section.attr('id', 'tab-' + tabId)

    if first
      first = false
      $section.addClass 'active-tab'

    tabId++

  first = true

  $ul.find('a').each () ->
    $a = $ this

    if first
      first = false
      $a.addClass 'active-tab-header'
      
    $a.click () ->

      $('.active-tab').removeClass 'active-tab'
      $('.active-tab-header').removeClass 'active-tab-header'

      $this = $ this

      $this.addClass 'active-tab-header'

      $section = $('#' + this.id.replace('tab-header', 'tab'))

      $section.addClass 'active-tab'

      false

$.fn.slideItemsIn = () ->
  
  delay = 0

  setTimeout(() =>
    
    children = this.find '*'

    children.each () ->
      child = $ this

      child.setTransition({
        property: 'opacity, left',
        'timing-function': 'ease-in',
        duration: '0.3s, 0.3s',
        delay: delay + 'ms'
      });

      child.css('opacity', 1)
      child.css('left', '0%')

      delay += 30

      return undefined
    return undefined
  , 0)

  return undefined