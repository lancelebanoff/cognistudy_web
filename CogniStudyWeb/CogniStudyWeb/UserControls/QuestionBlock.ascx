<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuestionBlock.ascx.cs" Inherits="CogniTutor.UserControls.QuestionBlock" %>

<script>
    function showCorrect(outer, explanation) {
        $('#' + outer + ' > a').each(function (i) {
            $(this).removeClass('btn-default');
            if ($(this).attr('correct') == 'True') {
                $(this).addClass('btn-success');
            }
            else {
                $(this).addClass('btn-danger');
            }
        });
        $('#' + explanation).removeClass('hidden');
    }
</script>

<div class="row">
    <h3 class="align-center">Question</h3><br />
    <div class="col-lg-6 text-center" style="padding-right:20px; border-right: 1px solid #ccc;">
        <img src="<%= QuestionContents.Keys.Contains("image") ? QuestionContents.Get<Parse.ParseFile>("image").Url.ToString() : "" %>" style="max-width:100%; max-height:100%;"/>
        <%= QuestionContents.Get<string>("questionText") %>
    </div>
    <div class="col-lg-6" id="divAnswers<%= Index %>">
        <% for (int i = 0; i < QuestionContents.Get<IList<object>>("answers").Count; i++) { %>
        <a class="btn btn-default btn-block" correct="<%= QuestionContents.Get<int>("correctAnswer") == i %>" onclick="showCorrect('divAnswers<%= Index %>','divExplanation<%= Index %>');">
            <%= QuestionContents.Get<List<object>>("answers")[i].ToString() %>
        </a>
        <% } %>
    </div>
</div>
<br />
<div class="row hidden" id="divExplanation<%= Index %>">
    <div class="col-lg-2" style="padding-right:20px; border-right: 1px solid #ccc;">
        <p class="align-center">Explanation</p>
    </div>
    <div class="col-lg-10">
        <%= QuestionContents.Get<string>("explanation") %>
    </div>
</div>
<br /><hr />