/*
 *  Parameters:
 *  
 *      String calendarType. Possible values:
 *          month_membership
 *          single_session
 */

$(document).ready(function () {

    showCalendar();

    //$('.fc-event-container').addClass('hidden');

    function showCalendar() {
        $('.calendar').each(function (index, calendar) {
            month_chooser_id = $(calendar).data('month-chooser');
            month_chooser = $('#' + month_chooser_id);
            month_start_time = $(month_chooser).val();
            selectedMonth = new moment(month_start_time);

            if (month_start_time === '--None--') {
                return;
            }
            console.log(calendarType);
            if (calendarType === 'single_session') {
                $(calendar).fullCalendar({
                    defaultDate: selectedMonth,
                    editable: false,
                    eventLimit: false, // allow "more" link when too many events
                    events: [
                        // August 2015
                        {
                            title: '6-8 pm Math Session',
                            start: '2015-08-05',
                            end: '2015-08-05',
                        },
                        {
                            title: '2-4 pm Math Session',
                            start: '2015-08-09',
                            end: '2015-08-09',
                        },
                        {
                            title: '6-8 pm English Session',
                            start: '2015-08-12',
                            end: '2015-08-12',
                        },
                        {
                            title: '2-4 pm English Session',
                            start: '2015-08-16',
                            end: '2015-08-16',
                        },
                        {
                            title: '6-8 pm Reading Session',
                            start: '2015-08-19',
                            end: '2015-08-19',
                        },
                        {
                            title: '2-4 pm Reading / Science Session',
                            start: '2015-08-23',
                            end: '2015-08-23',
                        },
                        {
                            title: '6-8 pm Science Session',
                            start: '2015-08-26',
                            end: '2015-08-26',
                        },
                        {
                            title: '2-4 pm Open Subject Session',
                            start: '2015-08-30',
                            end: '2015-08-30',
                        }
                    ],
                    // disable clicking on urls
                    eventClick: function (event) {
                        if (event.url) {
                            return false;
                        }
                    },
                });
            }
            else if (calendarType === 'month_membership') {
                $(calendar).fullCalendar({
                    defaultDate: selectedMonth,
                    editable: false,
                    eventLimit: false, // allow "more" link when too many events
                    events: [
                        // August 2015
                        {
                            title: '6-8 pm Math Session',
                            start: '2015-08-05',
                            end: '2015-08-05',
                        },
                        {
                            title: '2-4 pm Math Session',
                            start: '2015-08-09',
                            end: '2015-08-09',
                        },
                        {
                            title: '6-8 pm English Session',
                            start: '2015-08-12',
                            end: '2015-08-12',
                        },
                        {
                            title: '2-4 pm English Session',
                            start: '2015-08-16',
                            end: '2015-08-16',
                        },
                        {
                            title: '6-8 pm Reading Session',
                            start: '2015-08-19',
                            end: '2015-08-19',
                        },
                        {
                            title: '2-4 pm Reading / Science Session',
                            start: '2015-08-23',
                            end: '2015-08-23',
                        },
                        {
                            title: '6-8 pm Science Session',
                            start: '2015-08-26',
                            end: '2015-08-26',
                        },
                        {
                            title: '2-4 pm Open Subject Session',
                            start: '2015-08-30',
                            end: '2015-08-30',
                        },/*
                        // September 2015
                        {
                            title: '2-4 pm Math Session',
                            start: '2015-09-06',
                            end: '2015-09-06',
                        },
                        {
                            title: '6-8 pm Math Session',
                            start: '2015-09-09',
                            end: '2015-09-09',
                        },
                        {
                            title: '2-4 pm English Session',
                            start: '2015-09-13',
                            end: '2015-09-13',
                        },
                        {
                            title: '6-8 pm English Session',
                            start: '2015-09-16',
                            end: '2015-09-16',
                        },
                        {
                            title: '2-4 pm Reading Session',
                            start: '2015-09-20',
                            end: '2015-09-20',
                        },
                        {
                            title: '6-8 pm Reading / Science Session',
                            start: '2015-09-23',
                            end: '2015-09-23',
                        },
                        {
                            title: '2-4 pm Science Session',
                            start: '2015-09-27',
                            end: '2015-09-27',
                        },
                        {
                            title: '6-8 pm Open Subject Session',
                            start: '2015-09-30',
                            end: '2015-09-300',
                        },
                        // October 2015
                        {
                            title: '2-4 pm Math Session',
                            start: '2015-10-04',
                            end: '2015-10-04',
                        },
                        {
                            title: '6-8 pm Math Session',
                            start: '2015-10-07',
                            end: '2015-10-07',
                        },
                        {
                            title: '2-4 pm English Session',
                            start: '2015-10-11',
                            end: '2015-10-11',
                        },
                        {
                            title: '6-8 pm English Session',
                            start: '2015-10-14',
                            end: '2015-10-14',
                        },
                        {
                            title: '2-4 pm Reading Session',
                            start: '2015-10-18',
                            end: '2015-10-18',
                        },
                        {
                            title: '6-8 pm Reading / Science Session',
                            start: '2015-10-21',
                            end: '2015-10-21',
                        },
                        {
                            title: '2-4 pm Science Session',
                            start: '2015-10-25',
                            end: '2015-10-25',
                        },
                        {
                            title: '6-8 pm Open Subject Session',
                            start: '2015-10-28',
                            end: '2015-10-28',
                        },
                        // November 2015
                        {
                            title: '6-8 pm Math Session',
                            start: '2015-11-04',
                            end: '2015-11-04',
                        },
                        {
                            title: '2-4 pm Math Session',
                            start: '2015-11-08',
                            end: '2015-11-08',
                        },
                        {
                            title: '6-8 pm English Session',
                            start: '2015-11-11',
                            end: '2015-11-11',
                        },
                        {
                            title: '2-4 pm English Session',
                            start: '2015-11-15',
                            end: '2015-11-15',
                        },
                        {
                            title: '6-8 pm Reading Session',
                            start: '2015-11-18',
                            end: '2015-11-18',
                        },
                        {
                            title: '2-4 pm Reading / Science Session',
                            start: '2015-11-22',
                            end: '2015-11-22',
                        },
                        {
                            title: '6-8 pm Science Session',
                            start: '2015-11-25',
                            end: '2015-11-25',
                        },
                        {
                            title: '2-4 pm Open Subject Session',
                            start: '2015-11-29',
                            end: '2015-11-29',
                        },
                        // December 2015
                        {
                            title: '2-4 pm Math Session',
                            start: '2015-12-06',
                            end: '2015-12-06',
                        },
                        {
                            title: '6-8 pm Math Session',
                            start: '2015-12-09',
                            end: '2015-12-09',
                        },
                        {
                            title: '2-4 pm English Session',
                            start: '2015-12-13',
                            end: '2015-12-13',
                        },
                        {
                            title: '6-8 pm English Session',
                            start: '2015-12-16',
                            end: '2015-12-16',
                        },
                        {
                            title: '2-4 pm Reading Session',
                            start: '2015-12-20',
                            end: '2015-12-20',
                        },
                        {
                            title: '6-8 pm Reading / Science Session',
                            start: '2015-12-23',
                            end: '2015-12-23',
                        },
                        {
                            title: '2-4 pm Science Session',
                            start: '2015-12-27',
                            end: '2015-12-27',
                        },
                        {
                            title: '6-8 pm Open Subject Session',
                            start: '2015-12-30',
                            end: '2015-12-30',
                        },
                        // January 2016
                        {
                            title: '2-4 pm Math Session',
                            start: '2016-01-06',
                            end: '2016-01-06',
                        },
                        {
                            title: '6-8 pm Math Session',
                            start: '2016-01-10',
                            end: '2016-01-10',
                        },
                        {
                            title: '2-4 pm English Session',
                            start: '2016-01-13',
                            end: '2016-01-13',
                        },
                        {
                            title: '6-8 pm English Session',
                            start: '2016-01-17',
                            end: '2016-01-17',
                        },
                        {
                            title: '2-4 pm Reading Session',
                            start: '2016-01-20',
                            end: '2016-01-20',
                        },
                        {
                            title: '6-8 pm Reading / Science Session',
                            start: '2016-01-24',
                            end: '2016-01-24',
                        },
                        {
                            title: '2-4 pm Science Session',
                            start: '2016-01-27',
                            end: '2016-01-27',
                        },
                        {
                            title: '6-8 pm Open Subject Session',
                            start: '2016-01-31',
                            end: '2016-01-31',
                        },
                        // February 2016
                        {
                            title: '6-8 pm Math Session',
                            start: '2016-02-03',
                            end: '2016-02-03',
                        },
                        {
                            title: '2-4 pm Math Session',
                            start: '2016-02-07',
                            end: '2016-02-07',
                        },
                        {
                            title: '6-8 pm English Session',
                            start: '2016-02-10',
                            end: '2016-02-10',
                        },
                        {
                            title: '2-4 pm English Session',
                            start: '2016-02-14',
                            end: '2016-02-14',
                        },
                        {
                            title: '6-8 pm Reading Session',
                            start: '2016-02-17',
                            end: '2016-02-17',
                        },
                        {
                            title: '2-4 pm Reading / Science Session',
                            start: '2016-02-21',
                            end: '2016-02-21',
                        },
                        {
                            title: '6-8 pm Science Session',
                            start: '2016-02-24',
                            end: '2016-02-24',
                        },
                        {
                            title: '2-4 pm Open Subject Session',
                            start: '2016-02-28',
                            end: '2016-02-28',
                        },
                        // March 2016
                        {
                            title: '2-4 pm Math Session',
                            start: '2016-03-06',
                            end: '2016-03-06',
                        },
                        {
                            title: '6-8 pm Math Session',
                            start: '2016-03-09',
                            end: '2016-03-09',
                        },
                        {
                            title: '2-4 pm English Session',
                            start: '2016-03-13',
                            end: '2016-03-13',
                        },
                        {
                            title: '6-8 pm English Session',
                            start: '2016-03-16',
                            end: '2016-03-16',
                        },
                        {
                            title: '2-4 pm Reading Session',
                            start: '2016-03-20',
                            end: '2016-03-20',
                        },
                        {
                            title: '6-8 pm Reading / Science Session',
                            start: '2016-03-23',
                            end: '2016-03-23',
                        },
                        {
                            title: '2-4 pm Science Session',
                            start: '2016-03-27',
                            end: '2016-03-27',
                        },
                        {
                            title: '6-8 pm Open Subject Session',
                            start: '2016-03-30',
                            end: '2016-03-30',
                        },
                        // April 2016
                        {
                            title: '2-4 pm Math Session',
                            start: '2016-04-03',
                            end: '2016-04-03',
                        },
                        {
                            title: '6-8 pm Math Session',
                            start: '2016-04-06',
                            end: '2016-04-06',
                        },
                        {
                            title: '2-4 pm English Session',
                            start: '2016-04-10',
                            end: '2016-04-10',
                        },
                        {
                            title: '6-8 pm English Session',
                            start: '2016-04-13',
                            end: '2016-04-13',
                        },
                        {
                            title: '2-4 pm Reading Session',
                            start: '2016-04-17',
                            end: '2016-04-17',
                        },
                        {
                            title: '6-8 pm Reading / Science Session',
                            start: '2016-04-20',
                            end: '2016-04-20',
                        },
                        {
                            title: '2-4 pm Science Session',
                            start: '2016-04-24',
                            end: '2016-04-24',
                        },
                        {
                            title: '6-8 pm Open Subject Session',
                            start: '2016-04-27',
                            end: '2016-04-27',
                        },
                        // May 2016
                        {
                            title: '6-8 pm Math Session',
                            start: '2016-05-04',
                            end: '2016-05-04',
                        },
                        {
                            title: '2-4 pm Math Session',
                            start: '2016-05-08',
                            end: '2016-05-08',
                        },
                        {
                            title: '6-8 pm English Session',
                            start: '2016-05-11',
                            end: '2016-05-11',
                        },
                        {
                            title: '2-4 pm English Session',
                            start: '2016-05-15',
                            end: '2016-05-15',
                        },
                        {
                            title: '6-8 pm Reading Session',
                            start: '2016-05-18',
                            end: '2016-05-18',
                        },
                        {
                            title: '2-4 pm Reading / Science Session',
                            start: '2016-05-22',
                            end: '2016-05-22',
                        },
                        {
                            title: '6-8 pm Science Session',
                            start: '2016-05-25',
                            end: '2016-05-25',
                        },
                        {
                            title: '2-4 pm Open Subject Session',
                            start: '2016-05-29',
                            end: '2016-05-29',
                        },
                        // June 2016
                        {
                            title: '2-4 pm Math Session',
                            start: '2016-06-05',
                            end: '2016-06-05',
                        },
                        {
                            title: '6-8 pm Math Session',
                            start: '2016-06-08',
                            end: '2016-06-08',
                        },
                        {
                            title: '2-4 pm English Session',
                            start: '2016-06-12',
                            end: '2016-06-12',
                        },
                        {
                            title: '6-8 pm English Session',
                            start: '2016-06-15',
                            end: '2016-06-15',
                        },
                        {
                            title: '2-4 pm Reading Session',
                            start: '2016-06-19',
                            end: '2016-06-19',
                        },
                        {
                            title: '6-8 pm Reading / Science Session',
                            start: '2016-06-22',
                            end: '2016-06-22',
                        },
                        {
                            title: '2-4 pm Science Session',
                            start: '2016-06-26',
                            end: '2016-06-26',
                        },
                        {
                            title: '6-8 pm Open Subject Session',
                            start: '2016-06-29',
                            end: '2016-06-29',
                        },
                        // July 2016
                        {
                            title: '6-8 pm Math Session',
                            start: '2016-07-06',
                            end: '2016-07-06',
                        },
                        {
                            title: '2-4 pm Math Session',
                            start: '2016-07-10',
                            end: '2016-07-10',
                        },
                        {
                            title: '6-8 pm English Session',
                            start: '2016-07-13',
                            end: '2016-07-13',
                        },
                        {
                            title: '2-4 pm English Session',
                            start: '2016-07-17',
                            end: '2016-07-17',
                        },
                        {
                            title: '6-8 pm Reading Session',
                            start: '2016-07-20',
                            end: '2016-07-20',
                        },
                        {
                            title: '2-4 pm Reading / Science Session',
                            start: '2016-07-24',
                            end: '2016-07-24',
                        },
                        {
                            title: '6-8 pm Science Session',
                            start: '2016-07-27',
                            end: '2016-07-27',
                        },
                        {
                            title: '2-4 pm Open Subject Session',
                            start: '2016-07-31',
                            end: '2016-07-31',
                        },*/
                    ],
                    // disable clicking on urls
                    eventClick: function (event) {
                        if (event.url) {
                            return false;
                        }
                    },
                });
            }

            // Set title
            title_div = $(calendar).find('.fc-toolbar > .fc-left > h2');
            $(title_div).text('Upcoming Sessions in ' + $(title_div).text());

            // Loop through the events in the calendar
            $($(calendar).find('.fc-week')).each(function (week_index, week_element) {
                day_numbers = $(week_element).find('.fc-day-number');
                events = $($(week_element).find('tbody')[1]).find('tr > td');
                $(day_numbers).each(function (day_index, day_element) {
                    // Associate events with dates
                    $($(events[day_index])).find('a').attr('date', $(day_element).data('date'));
                    // Hide events that are not in the current month
                    if ($(day_element).hasClass('fc-other-month')) {
                        $(events[day_index]).addClass('invisible');
                    }
                });
            });
        });
    }

    $('.fc-event').on('click', function () {
        onEventClick(this);
    });

    function onEventClick(item) {
        if (calendarType === "month_membership WE SHOULD MAKE THIS SHOULD WORK IN THE FUTURE") {
            currentItems = $('#CalendarSelectedItems').attr('value');
            thisSession = $(item).attr('date');

            if ($(item).hasClass('fc-event-selected')) {
                $(item).removeClass('fc-event-selected');
                $('#CalendarSelectedItems').attr('value', currentItems.replace(thisSession + "\t", ""));
            }
            else {
                $(item).addClass('fc-event-selected');
                $('#CalendarSelectedItems').attr('value', currentItems + thisSession + "\t");
            }
        }
        else if (calendarType === "single_session") {
            thisSession = $(item).attr('date');
            $('.fc-event-selected').removeClass('fc-event-selected');
            $(item).addClass('fc-event-selected');
            $('#CalendarSelectedDates').attr('value', thisSession);
            $('#CalendarSelectedItems').attr('value', $(item).text());
        }
    }

});