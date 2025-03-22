namespace PersonalPageWASM.Data
{
    public static class CodeSnippets
    {
        public static string BasicBind()
        {
            return """
                <input type=""text""  @bind=""name"" />

                @code{
                    private string? name;
                }
                """;
        }

        public static string HTMLBind()
        {
            return """
                <input type="text"  value="@surname" @onchange="@((ChangeEventArgs args) => surname = args?.Value?.ToString())" />

                @code{
                    private string? surname;
                }
                """;
        }

        public static string OnInputBind()
        {
            return """
                <input type="text" @bind="nickname" @bind:event="oninput" />

                @code{
                    private string? nickname;
                }
                """;
        }

        public static string BindAfter()
        {
            return """
                <span class="@toggleClass">Execute additional logic during binding using &#64 bind:after="FunctionName": </span>
                <input type="text" @bind="nickname" @bind:after="ToggleCssClass" />
                <span class="text-danger"> @nickname</span>

                @code{
                    private string? nickname;
                    string toggleClass = "text-dark";

                    private void ToggleCssClass()
                    {
                        toggleClass = toggleClass == "text-dark" ? "text-primary" : "text-dark";
                    }
                }
                """;
        }

        public static string BindValue()
        {
            return """
                <InputText @bind-Value="petName" />

                @code{
                    private string? petName;
                }
                """;
        }

        public static string BindCustomComponent()
        {
            return """
                <InputGroupText @bind-Value="catName" PrependText="TEST" />

                @code{
                    private string? catName;
                }
                """;
        }

        public static string CustomBindingComponent()
        {
            return """
                <div class="d-flex input-group justify-content-center">
                    <div class="input-group-prepend w-65">
                        <span class="input-group-text w-100">@PrependText</span>
                    </div>
                    <input type="text" class="form-control" @bind="Value" minlength="@MinLength" maxlength="@MaxLength" disabled="@Disabled" required="@Required" />
                </div>


                @code {
                    private string _value;

                    [Parameter]
                    public string Value
                    {
                        get => _value;
                        set
                        {
                            if (_value == value) return;
                            _value = value;
                            ValueChanged.InvokeAsync(_value);
                        }
                    }

                    [Parameter]
                    public EventCallback<string?> ValueChanged { get; set; }

                    [Parameter]
                    public string PrependText { get; set; } = string.Empty;

                    [Parameter]
                    public bool Disabled { get; set; } = false;

                    [Parameter]
                    public bool Required { get; set; } = false;

                    [Parameter]
                    public string MinLength { get; set; } = string.Empty;

                    [Parameter]
                    public string MaxLength { get; set; } = string.Empty;
                }
                """;
        }
    }
}
